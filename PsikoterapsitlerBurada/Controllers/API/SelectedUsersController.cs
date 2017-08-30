using PsikoterapsitlerBurada.DTOs;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Repositories;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    public class SelectedUsersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
       

        public SelectedUsersController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpPost]
        public IHttpActionResult AddUsersToTheQuestion(SelectedUserDto userDto)
        {
            if (userDto.SelectedUsersId.Length > 3)
                return BadRequest("3 kişiden fazla seçim yapılamaz");

            var question = _unitOfWork.Questions.GetQuestionByQuestionId(userDto.QuestionId);
            var notification = new Notification()
            {
                Question = question,
                NotificationType = NotificationType.Question
            };

            foreach (var userId in userDto.SelectedUsersId)
            {
                var selectedUser = _unitOfWork.Users.GetUserById(userId);

                question?.AskedToWhom.Add(selectedUser);

                selectedUser?.Notify(notification);
            }
            
            _unitOfWork.Complate();
            return Ok();
        }

    }
}
