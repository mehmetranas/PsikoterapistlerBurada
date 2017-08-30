using PsikoterapsitlerBurada.DTOs;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Repositories;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    public class SelectedUsersController : ApiController
    {
        private readonly QuestionRepository _questionRepository;
        private readonly UserRepository _userRepository;
        private readonly UnitOfWork _unitOfWork;
       

        public SelectedUsersController()
        {
            var context = new ApplicationDbContext();
            _questionRepository = new QuestionRepository(context);
            _userRepository = new UserRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpPost]
        public IHttpActionResult AddUsersToTheQuestion(SelectedUserDto userDto)
        {
            if (userDto.SelectedUsersId.Length > 3)
                return BadRequest("3 kişiden fazla seçim yapılamaz");

            var question = _questionRepository.GetQuestionByQuestionId(userDto.QuestionId);
            var notification = new Notification()
            {
                Question = question,
                NotificationType = NotificationType.Question
            };

            foreach (var userId in userDto.SelectedUsersId)
            {
                var selectedUser = _userRepository.GetUserById(userId);

                question?.AskedToWhom.Add(selectedUser);

                selectedUser?.Notify(notification);
            }
            
            _unitOfWork.Complate();
            return Ok();
        }

    }
}
