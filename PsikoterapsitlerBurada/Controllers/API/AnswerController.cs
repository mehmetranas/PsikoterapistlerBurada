using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.DTOs;
using System;
using System.Web.Http;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class AnswerController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
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
