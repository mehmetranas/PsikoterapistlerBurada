﻿using System.Linq;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;

namespace PsikoterapsitlerBurada.Persistence.Repositories
{
    public class VoteRepository : IVoteRepository
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