using PsikoterapsitlerBurada.DTOs;
using PsikoterapsitlerBurada.Models;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    public class SelectedUsersController : ApiController
    {
        private ApplicationDbContext _context;

        public SelectedUsersController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult AddUsersToTheQuestion(SelectedUserDto UserDto)
        {
            var question = _context.Questions.SingleOrDefault(q => q.Id == UserDto.QuestionId);
            var notification = new Notification()
            {
                Question = question,
                NotificationType = NotificationType.Question
            };

            foreach (var userId in UserDto.SelectedUsersId)
            {
                var selectedUser = _context.Users.SingleOrDefault(u => u.Id == userId);

                question.AskedToWhom.Add(selectedUser);

                selectedUser.Notify(notification);
            }

            
            
            _context.SaveChanges();
            return Ok();
        }

    }
}
