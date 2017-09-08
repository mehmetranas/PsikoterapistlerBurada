using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    public class LikeController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public LikeController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        [Authorize]
        [HttpPost]
        public IHttpActionResult Create(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = _unitOfWork.Users.GetUserById(userId);
            var answer = _unitOfWork.Answers.GetAnswerById(id);
            if (answer == null || user == null || answer.User.Id == userId) return BadRequest();

            answer.Likes.Add(user);

            var notification = new Notification()
            {
                NotificationType = NotificationType.Like,
                UserLike = user,
                Answer = answer
            };

            answer.User.Notify(notification);

            _unitOfWork.Complete();

            return Ok();
        }

        [Authorize]
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = _unitOfWork.Users.GetUserById(userId);
            var answer = _unitOfWork.Answers.GetAnswerById(id);
            if (answer == null || user == null || !(user.LikesAnswers.Any(a => a.Id == answer.Id)))
                return BadRequest("Zaten like");
             
            var notification = _unitOfWork.Notifications
                .GetNotificationByUserLikeAndAnswer(userId, answer.Id);

            if (notification != null) _unitOfWork.Notifications.Remove(notification);

            user.LikesAnswers.Remove(answer);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpGet]
        public IEnumerable<int> GetUserLikesAnswersId(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = _unitOfWork.Users.GetUserById(userId);

            if (user == null) return null;

            var likesAnswersId = _unitOfWork.Users
                .GetUserLikeAnswersIdByQuestionId(user, id);

            return likesAnswersId;
        }
    }
}
