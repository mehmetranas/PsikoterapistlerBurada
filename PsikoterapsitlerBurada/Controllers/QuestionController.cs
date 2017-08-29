using AutoMapper;
using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Models.ViewModels;
using PsikoterapsitlerBurada.Repositories;
using System;
using System.Linq;
using System.Web.Mvc;

namespace PsikoterapsitlerBurada.Controllers
{
    public class QuestionController : Controller
    {
        private ApplicationDbContext _context;
        private CategoryRepository _categoryRepository;
        private UserRepository _userRepository;
        private QuestionRepository _questionRepository;

        public QuestionController()
        {
            _context = new ApplicationDbContext();
            _categoryRepository = new CategoryRepository(_context);
            _userRepository = new UserRepository(_context);
            _questionRepository = new QuestionRepository(_context);
        }

        // GET: Question
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new QuestionViewModel()
            {
                Categories = _categoryRepository.GetCategories(),
                AskedToWhom = _userRepository.GetAllUsers() //There is a problem that get all users properties, only get username and Id
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(QuestionViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = _userRepository.GetUserById(userId);
            var category = _categoryRepository.GetCategoryByCategoryId(model.Category.Id);

            var question = new Question()
            {
                QuestionText = model.QuestionText,
                DateTime = DateTime.Now,
                WhoAsked = user,
                Category = category
            };

            _context.Questions.Add(question);

            _context.SaveChanges();

            var questionId = _questionRepository.GetAllQuestions().Max(q => q.Id);
            return RedirectToAction("SelectUserToAskQuestion", new {id = questionId});
        }

        

        [Authorize]
        public ActionResult SelectUserToAskQuestion(int id)
        {
            var askedToWhom = _questionRepository.GetQuestionByQuestionId(id).AskedToWhom;

            if (askedToWhom.Count != 0)
            {
                return HttpNotFound();
            }

            var question = _questionRepository.GetQuestionByQuestionId(id);
            var questionViewModel = Mapper.Map<QuestionViewModel>(question);
            var userId = User.Identity.GetUserId();
            var users = _userRepository.GetAllUsersWithOutAuthUser(userId); //There is a problem that get all users properties, only get username, ctg. and rating
            var viewModel = new SelectUserToAskQuestionViewModel()
            {
                Users = users,
                Question = questionViewModel
            };
            return View(viewModel);
        }
    }
 }