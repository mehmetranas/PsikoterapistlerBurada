using AutoMapper;
using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Models.ViewModels;
using PsikoterapsitlerBurada.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Mvc;

namespace PsikoterapsitlerBurada.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private readonly QuestionRepository _questionRepository;
        private readonly UserRepository _userRepository;
        private readonly FollowingRepository _followingRepository;

        public UserController()
        {
            var context = new ApplicationDbContext();
            _questionRepository = new QuestionRepository(context);
            _userRepository = new UserRepository(context);
            _followingRepository = new FollowingRepository(context);
        }

        public ActionResult GetMyQuestions()
        {
            var userId = User.Identity.GetUserId();
            var myQuestions = _questionRepository
                .GetUserQuestions(userId)
                .Select(Mapper.Map<QuestionViewModel>);

            return View(myQuestions);
        }

        public ActionResult GetUnAskedQuestions()
        {
            var userId = User.Identity.GetUserId();
            var questions = _questionRepository
                .GetUserUnAskedQuestions(userId)
                .Select(Mapper.Map<QuestionViewModel>);

            return View(questions);
        }

       

        public ActionResult GetQustionAskedToMe()
        {
            var userId = User.Identity.GetUserId();
            var user = _userRepository.GetUserById(userId);

            var questions = _questionRepository
                .GetQuestionsAskedToUser(userId)
                .OrderBy(d => d.DateTime);

            if (user == null) return HttpNotFound();

            return View(questions);
        }

      [Authorize]
        public ActionResult WriteAnswer(int id)
        {
            var question = _questionRepository
                .GetQuestionByQuestionId(id);

            var viewModel = new AnswerViewModel()
            {
                Question = question
            };

            return View(viewModel);
        }

        public ActionResult UserProfile(string id)
        {
            var authUserId = User.Identity.GetUserId();
            var user = _userRepository
                .GetUsers(id);

            var viewModel = new ProfileViewModel(authUserId, user);
           
            return View(viewModel);
        }

        

        public ActionResult GetUserQuestions(string id)
        {
            var userQuestions = _questionRepository
                .GetUserQuestionsWithWhoAskAskToWhomAns(id)
                .Select(Mapper.Map<QuestionViewModel>);

            return PartialView("_UserAskedQuestions", userQuestions);
        }

        public ActionResult GetUserQuestionsAsked(string id)
        {
            var userQuestions = _questionRepository
                .GetUsersQuestionsAskedWithAns(id)
                .Select(Mapper.Map<QuestionViewModel>);

            return PartialView("_UserQuestionsAsked", userQuestions);
        }

        public ActionResult GetUserFollowers(string id)
        {
            var followers = _followingRepository
                .GetFollowingsByFollower(id);

            var authUserId = User.Identity.GetUserId();

            var viewModel = new List<ProfileViewModel>();

            foreach (var follower in followers)
            {
                var profileViewModel = new ProfileViewModel(authUserId,follower.Followee);
               
                viewModel.Add(profileViewModel);
            }

            return PartialView("_UserProfiles", viewModel);
        }

        public ActionResult GetUserFollowees(string id)
        {

            var followees = _followingRepository
                .GetFollowingsByFollowee(id);

            var authUserId = User.Identity.GetUserId();

            var viewModel = new List<ProfileViewModel>();

            foreach (var followee in followees)
            {
                var profileViewModel = new ProfileViewModel(authUserId, followee.Follower);

                viewModel.Add(profileViewModel);
            }

            return PartialView("_UserProfiles", viewModel);
        }

       

        public ActionResult GetUserFavoriteQuestions(string id)
        {
            var favoriteQuestions = _questionRepository
                .GetUserFavoriteQuestionsWithAnsAskToWhomWhoAsk(id)
                .OrderByDescending(q => q.DateTime)
                .Select(Mapper.Map<QuestionViewModel>);

            return PartialView("_UserAskedQuestions", favoriteQuestions);
        }
    }
}