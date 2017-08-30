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
        private readonly QuestionRepository _questionRepository;
        private readonly AnswerRepository _answerRepository;
        private readonly UnitOfWork _unitOfWork;

        public AnswerController()
        {
            var context = new ApplicationDbContext();
            _questionRepository = new QuestionRepository(context);
            _answerRepository = new AnswerRepository(context);
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

            _answerRepository.Add(currentAnswer);

            var notification = new Notification()
            {
                Answer = currentAnswer,
                NotificationType = NotificationType.Answer
            };

            var whoAsked = _questionRepository
                .GetQuestionByQuestionId(answerDto.QuestionId)
                .WhoAsked;

            whoAsked.Notify(notification);

            _unitOfWork.Complate();
            return Ok();
        }
    }
}
