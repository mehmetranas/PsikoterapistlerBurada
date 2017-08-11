using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using System;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PsikoterapsitlerBurada.Controllers
{
    public class QuestionController : Controller
    {
        private ApplicationDbContext _context;

        public QuestionController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Question
        [System.Web.Mvc.Authorize]
        public ActionResult Create()
        {
            QuestionViewModel viewModel = new QuestionViewModel()
            {
                Categories = _context.Categories.ToList(),
                AskedToWhom = _context.Users.ToList() //There is a problem that get all users properties, only get username and Id
            };
            return View(viewModel);
        }

        [Authorize]
        [HttpPost]
        public ActionResult Create(QuestionViewModel model)
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);
            var category = _context.Categories.SingleOrDefault(c => c.Id == model.Category.Id);

            var question = new Question()
            {
                QuestionText = model.QuestionText,
                DateTime = DateTime.Now,
                WhoAsked = user,
                Category = category
            };

            _context.Questions.Add(question);
            _context.SaveChanges();
            var questionId = _context.Questions.Max(q => q.Id);
            return RedirectToAction("SelectUserToAskQuestion", new {id = questionId});
        }

        [Authorize]
        public ActionResult SelectUserToAskQuestion(int id)
        {
            var users = _context.Users.ToList(); //There is a problem that get all users properties, only get username, ctg. and rating
            var question = _context.Questions.SingleOrDefault(q => q.Id == id);
            var viewModel = new SelectUserToAskQuestionViewModel()
            {
                Users = users,
                Question = question
            };
            return View(viewModel);
        }

        [Authorize]
        public ActionResult GetMyQuestions()
        {
            var userId = User.Identity.GetUserId();
            var myQuestions = _context.Questions.Where(q => q.WhoAsked.Id == userId).Include("WhoAsked").Include("AskedToWhom").ToList();
            return View(myQuestions);
        }

        public ActionResult GetUnAskedQuestions()
        {
            var userId = User.Identity.GetUserId();
            var questions = _context.Questions.Where(q => q.WhoAsked.Id == userId && q.AskedToWhom.Count == 0);
            return View(questions);
        }
    }
}