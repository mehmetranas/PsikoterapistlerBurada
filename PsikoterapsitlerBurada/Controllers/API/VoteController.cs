using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.DTOs;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Repositories;
using System;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class VoteController : ApiController
    {
        private readonly QuestionRepository _questionRepository;
        private readonly VoteRepository _voteRepository;
        private readonly UnitOfWork _unitOfWork;

        public VoteController( )
        {
            var context = new ApplicationDbContext();
            _questionRepository = new QuestionRepository(context);
            _voteRepository = new VoteRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpPost]
        public IHttpActionResult VoteAction(VoteDto voteDto)
        {
            var userId = User.Identity.GetUserId();
            var question = _questionRepository.GetQuestionByQuestionId(voteDto.QuestionId);


            var isVote = question != null && question.Votes.Any(v => v.UserId == userId);

            if (isVote)
            {
                var userVote = question.Votes.SingleOrDefault(v => v.UserId == userId);
                if (userVote != null)
                {
                    var userVoteState = userVote.VoteState;
                    var canVote = userVoteState + voteDto.VoteAction == 0 || userVoteState == 0;

                    if (!canVote)
                    {
                        return Json(new {isVoteUp = true});
                    }
                }

                if (userVote == null) return Ok();
                userVote.VoteState += voteDto.VoteAction;
                _unitOfWork.Complate();
                if (userVote.VoteState == 0) return Json(new {isRollBack = true});

                return Ok();
            }
           
            var vote = new Vote()
            {
                QuestionId = voteDto.QuestionId,
                UserId = userId,
                DateTime = DateTime.Now,
                VoteState = voteDto.VoteAction
            };

            _voteRepository.Add(vote);
            _unitOfWork.Complate();

            return Ok();
        }

        [System.Web.Http.HttpGet]
        public int GetVotes(int id)
        {
            var votes = _voteRepository.GetVotesByQuestion(id);
            return votes;
        }

    }
}
