using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PsikoterapsitlerBurada.DTOs;
using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Controllers.API
{
    public class RemoveSelectedUsersController : ApiController
    {
        private ApplicationDbContext _context;

        public RemoveSelectedUsersController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult RemoveSelectedUserFromTheQuestion(SelectedUserDto selectedUserDto)
        {
            var selectedUser = _context.Users.SingleOrDefault(u => u.Id == selectedUserDto.SelectedUserId);
            var question = _context.Questions.SingleOrDefault(q => q.Id == selectedUserDto.QuestionId);

            if (question == null || selectedUser == null)
            {
                return BadRequest("Hata oluştu, lütfen tekrar deneyin.");
            }

            selectedUser.QuestionsAsked.Remove(question);
            _context.SaveChanges();
            return Ok();
        }
    }
}
