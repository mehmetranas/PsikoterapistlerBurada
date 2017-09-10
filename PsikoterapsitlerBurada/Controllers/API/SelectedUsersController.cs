using PsikoterapsitlerBurada.Core.DTOs;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    public class SelectedUsersController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;
       

        public SelectedUsersController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        [HttpPost]
        public IHttpActionResult AddUsersToTheQuestion(SelectedUserDto userDto)
        {
            if (userDto.SelectedUsersId.Length > 3)
                return BadRequest("3 kişiden fazla seçim yapılamaz");
            if (userDto.SelectedUsersId.Length == 0)
                return BadRequest("En az bir kişi seçilmeli");

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

            _unitOfWork.Complete();
            return Ok();
        }

    }
}
