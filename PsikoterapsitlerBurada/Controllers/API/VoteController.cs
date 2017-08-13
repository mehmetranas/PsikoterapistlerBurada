using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using System;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class VoteController : ApiController
    {
        private ApplicationDbContext _context;

        public VoteController( )
        {
            _context = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult UpVote(int id)
        {
            var userId = User.Identity.GetUserId();
            var question = _context.Questions
                .Include("Votes")
                .SingleOrDefault(q => q.Id == id);

            var isVoteUp = question != null && question.Votes.Any(v => v.UserId == userId);

            if (isVoteUp)
            {
                return BadRequest("Daha önce oy kullanmışsınız.");
            }
           
            var vote = new Vote()
            {
                QuestionId = id,
                UserId = userId,
                DateTime = DateTime.Now,
                VoteState = 1
            };

            _context.Votes.Add(vote);
            _context.SaveChanges();

            return Ok();
        }

        [HttpGet]
        public int GetVotes(int id)
        {
            var votes = _context.Votes.Where(v => v.QuestionId == id).Sum(s => s.VoteState);
            return votes;
        }
    }
}
