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


            var isVoteUp = question != null && question.Votes.Any(v => v.UserId == userId);

            if (isVoteUp)
            {
                return Json(new {@isVoteUp = true});
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
