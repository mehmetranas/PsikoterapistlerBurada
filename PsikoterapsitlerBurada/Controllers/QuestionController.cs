using AutoMapper;
using Microsoft.AspNet.Identity;
using System;
using System.Linq;
using System.Web.Mvc;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Models.ViewModels;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;

namespace PsikoterapsitlerBurada.Controllers
{
    public class QuestionController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public QuestionController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        // GET: Question
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new QuestionViewModel()
            {
                Categories = _unitOfWork.Categories.GetCategories(),
                AskedToWhom = _unitOfWork.Users.GetAllUsers() //There is a problem that get all users properties, only get username and Id
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(QuestionViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = _unitOfWork.Users.GetUserById(userId);
            var category = _unitOfWork.Categories.GetCategoryByCategoryId(model.Category.Id);

            var question = new Question()
            {
                QuestionText = model.QuestionText,
                DateTime = DateTime.Now,
                WhoAsked = user,
                Category = category
            };

            _unitOfWork.Questions.Add(question);

            _unitOfWork.Complate();

            var questionId = _unitOfWork.Questions.GetAllQuestions().Max(q => q.Id);
            return RedirectToAction("SelectUserToAskQuestion", new {id = questionId});
        }

        

        [Authorize]
        public ActionResult SelectUserToAskQuestion(int id)
        {
            var askedToWhom = _unitOfWork.Questions.GetQuestionByQuestionId(id).AskedToWhom;

            if (askedToWhom.Count != 0)
            {
                return HttpNotFound();
            }

            var question = _unitOfWork.Questions.GetQuestionByQuestionId(id);
            var questionViewModel = Mapper.Map<QuestionViewModel>(question);
            var userId = User.Identity.GetUserId();
            var users = _unitOfWork.Users.GetAllUsersWithOutAuthUser(userId); //There is a problem that get all users properties, only get username, ctg. and rating
            var viewModel = new SelectUserToAskQuestionViewModel()
            {
                Users = users,
                Question = questionViewModel
            };
            return View(viewModel);
        }
    }
 }