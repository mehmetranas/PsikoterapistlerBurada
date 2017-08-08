using PsikoterapsitlerBurada.DTOs;
using PsikoterapsitlerBurada.Models;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers
{
    public class SelectedUsersController : ApiController
    {
        private ApplicationDbContext _context;

        public SelectedUsersController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult AddUsersToTheQuestion(SelectedUserDto selectedUserDto)
        {
            var selectedUser = _context.Users.SingleOrDefault(u => u.Id == selectedUserDto.SelectedUserId);
            var question = _context.Questions.SingleOrDefault(q => q.Id == selectedUserDto.QuestionId);

            if (question == null || selectedUser == null)
            {
                return BadRequest("Hata oluştu, lütfen tekrar deneyin.");
            }
            question.AskedToWhom.Add(selectedUser);
            _context.SaveChanges();
            return Ok("Tebrikler! Sorunuz Yolda.");
        }
    }
}
