using PsikoterapsitlerBurada.Models;
using System.Linq;

namespace PsikoterapsitlerBurada.Repositories
{
    public class VoteRepository
    {
        private readonly ApplicationDbContext _context;

        public VoteRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public int GetVotesByQuestion(int id)
        {
            return _context.Votes.Where(v => v.QuestionId == id).Sum(s => s.VoteState);
        }


        public void Add(Vote vote)
        {
            _context.Votes.Add(vote);
        }

    }
}