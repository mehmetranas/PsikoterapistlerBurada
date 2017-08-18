using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    public class FavoriteController : ApiController
    {
        private ApplicationDbContext _context;

        public FavoriteController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult FavoriteState(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.Find(userId);
            var question = _context.Questions.Find(id);
            var isFavorite = user.FavoriteQuestions.Any(q => q.Id == id);
            if (isFavorite)
            {
                user.FavoriteQuestions.Remove(question);
                _context.SaveChanges();
                return Json(new { action = "delete" });
            }
            user.FavoriteQuestions.Add(question);
            _context.SaveChanges();
            return Json(new { action = "add" });
        }

        [HttpGet]
        public IEnumerable<int> GetFavoriteQuestions()
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.Find(userId);
            var questionsId = user.FavoriteQuestions.Select(q => q.Id);

            return questionsId;
        }
    }
}
