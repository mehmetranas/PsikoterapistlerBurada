using Microsoft.AspNet.Identity;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class FavoriteController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public FavoriteController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
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
