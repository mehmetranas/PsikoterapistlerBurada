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
        public readonly IUnitOfWork _unitOfWork;

        public FavoriteController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpPost]
        public IHttpActionResult FavoriteState(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = _unitOfWork.Users.GetUserById(userId);
            var question = _unitOfWork.Questions.GetQuestionByQuestionId(id);
            var isFavorite = _unitOfWork.Users.GetUserFavoriteQuestions(userId).Contains(question);

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
            var questionsId = _unitOfWork.Users
                .GetUserFavoriteQuestions(User.Identity.GetUserId())
                .Select(q => q.Id);

            return questionsId;
        }
    }
}
