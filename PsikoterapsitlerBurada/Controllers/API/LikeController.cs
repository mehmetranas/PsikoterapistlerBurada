using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class LikeController : ApiController
    {
        private readonly UserRepository _userRepository;
        private readonly AnswerRepository _answerRepository;
        private readonly NotificationRepository _notificationRepository;
        private readonly UnitOfWork _unitOfWork;

        public LikeController()
        {
            var context = new ApplicationDbContext();
            _userRepository = new UserRepository(context);
            _answerRepository = new AnswerRepository(context);
            _notificationRepository = new NotificationRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpPost]
        public IHttpActionResult Create(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = _userRepository.GetUserById(userId);
            var answer = _answerRepository.GetAnswerById(id);
            if (answer == null || user == null || answer.User.Id == userId) return BadRequest();

            answer.Likes.Add(user);

            var notification = new Notification()
            {
                NotificationType = NotificationType.Like,
                UserLike = user,
                Answer = answer
            };

            answer.User.Notify(notification);

            _unitOfWork.Complate();

            return Ok();
        }
        
        [HttpDelete]
        public IHttpActionResult Delete(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = _userRepository.GetUserById(userId);
            var answer = _answerRepository.GetAnswerById(id);
            if (answer == null || user == null || !(user.LikesAnswers.Any(a => a.Id == answer.Id)))
                return BadRequest("Zaten like");
             
            var notification = _notificationRepository
                .GetNotificationByUserLikeAndAnswer(userId, answer.Id);

            if (notification != null) _notificationRepository.Remove(notification);

            user.LikesAnswers.Remove(answer);
            _unitOfWork.Complate();

            return Ok();
        }

        [HttpGet]
        public IEnumerable<int> GetUserLikesAnswersId(int id)
        {
            var userId = User.Identity.GetUserId();
            var user = _userRepository.GetUserById(userId);

            if (user == null) return null;

            var likesAnswersId = _userRepository
                .GetUserLikeAnswersIdByQuestionId(user, id);
            return likesAnswersId;
        }
    }
}
