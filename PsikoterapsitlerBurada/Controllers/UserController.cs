using AutoMapper;
using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Models.ViewModels;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PsikoterapsitlerBurada.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext _context;

        public UserController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult GetMyQuestions()
        {
            var userId = User.Identity.GetUserId();
            var myQuestions = _context.Questions.Where(q => q.WhoAsked.Id == userId).Include("WhoAsked")
                .Include("AskedToWhom").Select(Mapper.Map<QuestionViewModel>);

            return View(myQuestions);
        }

        public ActionResult GetUnAskedQuestions()
        {
            var userId = User.Identity.GetUserId();
            var questions = _context.Questions
                .Where(q => q.WhoAsked.Id == userId && q.AskedToWhom.Count == 0)
                .Select(Mapper.Map<QuestionViewModel>);

            return View(questions);
        }

        public ActionResult GetQustionAskedToMe()
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.
                SingleOrDefault(u => u.Id == userId);

            var questions = _context.Questions.Include("WhoAsked").Include("Answers").Where(q => q.AskedToWhom.Any(u => u.Id == userId)).ToList().OrderBy(d => d.DateTime);

            if (user == null) return HttpNotFound();

            return View(questions);
        }

        [Authorize]
        public ActionResult WriteAnswer(int id)
        {
            var question = _context.Questions.Include("WhoAsked").SingleOrDefault(q => q.Id == id);
            var viewModel = new AnswerViewModel()
            {
                Question = question
            };

            return View(viewModel);
        }

        public ActionResult UserProfile(string id)
        {
            var user = _context.Users
                .Include(u => u.Followees)
                .Include(u => u.Followers)
                .Include(u => u.FavoriteQuestions)
                .Include(u => u.LikesAnswers)
                .SingleOrDefault(u => u.Id == id);
            var authUserId = User.Identity.GetUserId();

            var viewModel = new ProfileViewModel(authUserId, user);
           
            return View(viewModel);
        }

        public ActionResult GetUserQuestions(string id)
        {
            var userQuestions = _context.Questions
                .Where(q => q.WhoAsked.Id == id)
                .Include(q => q.WhoAsked)
                .Include(q => q.AskedToWhom)
                .Include(q => q.Answers)
                .Select(Mapper.Map<QuestionViewModel>);

            return PartialView("_UserAskedQuestions", userQuestions);
        }

        public ActionResult GetUserQuestionsAsked(string id)
        {
            var userQuestions = _context.Questions.Include(q => q.WhoAsked)
                .Include(q => q.Answers)
                .Where(q => q.AskedToWhom.Any(u => u.Id == id))
                .Select(Mapper.Map<QuestionViewModel>);

            return PartialView("_UserQuestionsAsked", userQuestions);
        }

        public ActionResult GetUserFollowers(string id)
        {

            var followers = _context.Followings
                .Include(f => f.Followee)
                .Include(f => f.Follower)
                .Where(f => f.FollowerId == id);

            var authUserId = User.Identity.GetUserId();

            var viewModel = new List<ProfileViewModel>();

            foreach (var follower in followers)
            {
                var profileViewModel = new ProfileViewModel(authUserId,follower.Followee);
               
                viewModel.Add(profileViewModel);
            }

            return PartialView("_Following", viewModel);
        }

        public ActionResult GetUserFollowees(string id)
        {

            var followees = _context.Followings
                .Include(f => f.Followee)
                .Include(f => f.Follower)
                .Where(f => f.FolloweeId == id);

            var authUserId = User.Identity.GetUserId();

            var viewModel = new List<ProfileViewModel>();

            foreach (var followee in followees)
            {
                var profileViewModel = new ProfileViewModel(authUserId, followee.Follower);

                viewModel.Add(profileViewModel);
            }

            return PartialView("_Following", viewModel);
        }

        public ActionResult GetUserFavoriteQuestions(string id)
        {
            var user = _context.Users
                .Include(u => u.FavoriteQuestions)
                .SingleOrDefault(u => u.Id == id);

            if (user == null) return new HttpNotFoundResult();

            var favoriteQuestions = _context.Questions
                .Include(q => q.Answers)
                .Include(q => q.AskedToWhom)
                .Include(q => q.WhoAsked)
                .Where(q => q.UsersTrack.Any(u => u.Id == id)).Select(Mapper.Map<QuestionViewModel>);
            return PartialView("_UserAskedQuestions", favoriteQuestions);
        }
    }
}