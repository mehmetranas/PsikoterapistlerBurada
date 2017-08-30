using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class FavoriteController : ApiController
    {
        private readonly UserRepository _userRepository;
        public readonly QuestionRepository _questionRepository;
        public readonly UnitOfWork _unitOfWork;

        public FavoriteController()
        {
            var context = new ApplicationDbContext();
            _userRepository = new UserRepository(context);
            _questionRepository = new QuestionRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpPost]
        public IHttpActionResult FavoriteState(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = _userRepository.GetUserById(userId);
            var question = _questionRepository.GetQuestionByQuestionId(id);
            var isFavorite = _userRepository.GetUserFavoriteQuestions(userId).Contains(question);

            if (isFavorite)
            {
                user.FavoriteQuestions.Remove(question);
                _unitOfWork.Complate();
                return Json(new { action = "delete" });
            }

            user.FavoriteQuestions.Add(question);
            _unitOfWork.Complate();
            return Json(new { action = "add" });
        }

        [HttpGet]
        public IEnumerable<int> GetFavoriteQuestions()
        {
            var questionsId = _userRepository
                .GetUserFavoriteQuestions(User.Identity.GetUserId())
                .Select(q => q.Id);

            return questionsId;
        }
    }
}
