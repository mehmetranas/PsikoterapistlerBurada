using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

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
            var user = _unitOfWork.Users.GetUserById(User.Identity.GetUserId());
            var question = _unitOfWork.Questions.GetQuestionByQuestionId(id);
            var isFavorite = _unitOfWork.Users.GetUserFavoriteQuestions(User.Identity.GetUserId()).Contains(question);

            if (isFavorite)
            {
                user.FavoriteQuestions.Remove(question);
                _unitOfWork.Complete();
                return Json(new { action = "delete" });
            }

            user.FavoriteQuestions.Add(question);
            _unitOfWork.Complete();
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
