using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Repositories
{
    public class AnswerRepository : IAnswerRepository
    {
        private readonly ApplicationDbContext _context;

        public AnswerRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public ICollection<Answer> GetAnswersByQuestion(int id)
        {
            return _context.Answers.Where(a => a.Question.Id == id)
                .Include(a => a.Question)
                .Include(a => a.User)
                .Include(a => a.Likes)
                .ToList();

        }

        public Answer GetAnswerById(int id)
        {
            return _context.Answers
                .Include(a => a.User)
                .SingleOrDefault(a => a.Id == id);
        }

        public void Add(Answer answer)
        {
            _context.Answers.Add(answer);
        }

        public void Remove(Answer answer)
        {
            _context.Answers.Remove(answer);
        }
    }
}