using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.DTOs;
using PsikoterapsitlerBurada.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class AnswerController : ApiController
    {
        private ApplicationDbContext _context;

        public AnswerController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Create(AnswerDto answerDto)
        {
            var currentAnswer = new Answer()
            {
                AnswerText = answerDto.AnswerText,
                DateTime = DateTime.Now,
                QuestionId = answerDto.QuestionId,
                UserId = User.Identity.GetUserId()
            };

            _context.Answers.Add(currentAnswer);

            var notification = new Notification()
            {
                Answer = currentAnswer,
                NotificationType = NotificationType.Answer
            };

            var whoAsked = _context.Questions.Include("whoAsked").SingleOrDefault(q => q.Id == currentAnswer.QuestionId).WhoAsked;

            whoAsked.Notify(notification);

            _context.SaveChanges();
            return Ok();
        }
    }
}
