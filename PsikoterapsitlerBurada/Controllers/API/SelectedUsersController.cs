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
        public IHttpActionResult AddUsersToTheQuestion(SelectedUserDto[] UserDto)
        {
            foreach (var userDto in UserDto)
            {
                var selectedUser = _context.Users.SingleOrDefault(u => u.Id == userDto.SelectedUserId);
                var question = _context.Questions.SingleOrDefault(q => q.Id == userDto.QuestionId);

                question.AskedToWhom.Add(selectedUser);
            }
            
            _context.SaveChanges();
            return Ok();
        }

    }
}
