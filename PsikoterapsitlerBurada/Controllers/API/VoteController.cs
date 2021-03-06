﻿using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Core.DTOs;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Repositories;
using System;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class VoteController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public VoteController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        [HttpPost]
        public IHttpActionResult VoteAction(VoteDto voteDto)
        {
            var userId = User.Identity.GetUserId();
            var question = _unitOfWork.Questions.GetQuestionByQuestionId(voteDto.QuestionId);


            var isVote = question != null && question.Votes.Any(v => v.UserId == userId);

            if (isVote)
            {
                var userVote = question.Votes.SingleOrDefault(v => v.UserId == userId);
                if (userVote == null) return Ok();

                var userVoteState = userVote.VoteState;
                var canVote = userVoteState + voteDto.VoteAction == 0 || userVoteState == 0;

                if (!canVote)
                {
                    return Json(new { isVoteUp = true });
                }

                userVote.VoteState += voteDto.VoteAction;
                _unitOfWork.Complete();
                if (userVote.VoteState == 0) return Json(new { isRollBack = true });

                return Ok();
            }

            var vote = new Vote()
            {
                QuestionId = voteDto.QuestionId,
                UserId = userId,
                DateTime = DateTime.Now,
                VoteState = voteDto.VoteAction
            };

            _unitOfWork.Votes.Add(vote);
            _unitOfWork.Complete();

            return Ok();
        }

        [HttpGet]
        public int GetVotes(int id)
        {
            var votes = _unitOfWork.Votes.GetVotesByQuestion(id);
            return votes;
        }

    }
}
