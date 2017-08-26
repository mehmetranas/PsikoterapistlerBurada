using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.DTOs;
using PsikoterapsitlerBurada.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class VoteController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public VoteController( )
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult VoteAction(VoteDto voteDto)
        {
            var userId = User.Identity.GetUserId();
            var question = _context.Questions
                .Include("Votes")
                .SingleOrDefault(q => q.Id == voteDto.QuestionId);


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
                _context.SaveChanges();
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

            _context.Votes.Add(vote);
            _context.SaveChanges();

            return Ok();
        }

        [System.Web.Http.HttpGet]
        public int GetVotes(int id)
        {
            var votes = _context.Votes.Where(v => v.QuestionId == id).Sum(s => s.VoteState);
            return votes;
        }
    }
}
