using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class LikeController : ApiController
    {
        private ApplicationDbContext _contex;

        public LikeController()
        {
            _contex = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Create(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = _contex.Users.SingleOrDefault(u => u.Id == userId);
            var answer = _contex.Answers.SingleOrDefault(a => a.Id == id);
            if (answer == null || user == null) return BadRequest();

            answer.Likes.Add(user);
            _contex.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = _contex.Users.SingleOrDefault(u => u.Id == userId);
            var answer = _contex.Answers.SingleOrDefault(a => a.Id == id);
            if (answer == null || user == null || !(user.LikesAnswers.Any(a => a.Id == answer.Id))) return Json(new
            {
                user = user,
                answer = answer
                
            });

            user.LikesAnswers.Remove(answer);
            _contex.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public IEnumerable<int> GetUserLikesAnswersId(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = _contex.Users.SingleOrDefault(u => u.Id == userId);
            var likesAnswersId = user?.LikesAnswers.Where(a => a.QuestionId == id).Select(a => a.Id);

            return likesAnswersId;
        }
    }
}
