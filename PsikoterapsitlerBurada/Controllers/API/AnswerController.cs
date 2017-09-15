using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Core.DTOs;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;
using System;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class AnswerController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswerController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult Create(AnswerDto answerDto)
        {
            if (string.IsNullOrWhiteSpace(answerDto.AnswerText)) return BadRequest();

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

            _unitOfWork.Complete();
            return Ok();
        }
    }
}
