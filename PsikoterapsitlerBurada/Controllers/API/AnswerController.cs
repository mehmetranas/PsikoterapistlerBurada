using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.DTOs;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Repositories;
using System;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class AnswerController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
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

            _unitOfWork.Answers.Add(currentAnswer);

            var notification = new Notification()
            {
                Answer = currentAnswer,
                NotificationType = NotificationType.Answer
            };

            var whoAsked = _unitOfWork.Questions
                .GetQuestionByQuestionId(answerDto.QuestionId)
                .WhoAsked;

            whoAsked.Notify(notification);

            _unitOfWork.Complate();
            return Ok();
        }
    }
}
