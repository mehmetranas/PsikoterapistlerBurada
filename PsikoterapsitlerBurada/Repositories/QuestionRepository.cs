using PsikoterapsitlerBurada.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;

namespace PsikoterapsitlerBurada.Repositories
{
    public class QuestionRepository : IQuestionRepository
    {
        private readonly ApplicationDbContext _context;

        public QuestionRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Question GetQuestionByQuestionId(int id)
        {
            return _context.Questions
                .Include(q => q.AskedToWhom)
                .Include(q => q.WhoAsked)
                .SingleOrDefault(q => q.Id == id);
        }

        public IOrderedEnumerable<Question> GetAllQuestions()
        {
            return _context.Questions
                .Include(q => q.Category)
                .Include(q => q.WhoAsked)
                .Include(q => q.Votes)
                .Include(q => q.Answers)
                .Include(q => q.AskedToWhom)
                .ToList().OrderByDescending(q => q.DateTime);
        }

        public IOrderedQueryable<Question> GetUserQuestions(string userId)
        {
            return _context.Questions
                .Include(q => q.WhoAsked)
                .Include(q => q.AskedToWhom)
                .Include(q => q.Votes)
                .Include(q => q.Answers)
                .Where(q => q.WhoAsked.Id == userId)
                .OrderByDescending(q => q.DateTime);
        }

        public IQueryable<Question> GetUserUnAskedQuestions(string userId)
        {
            return _context.Questions
                .Where(q => q.WhoAsked.Id == userId && q.AskedToWhom.Count == 0);
        }

        public IQueryable<Question> GetQuestionsAskedToUser(string userId)
        {
            return _context.Questions
                .Include(q => q.WhoAsked)
                .Include(q => q.Answers)
                .Where(q => q.AskedToWhom.Any(u => u.Id == userId));
        }

        public IQueryable<Question> GetUserQuestionsWithWhoAskAskToWhomAns(string id)
        {
            return _context.Questions
                .Include(q => q.WhoAsked)
                .Include(q => q.AskedToWhom)
                .Include(q => q.Answers)
                .Where(q => q.WhoAsked.Id == id);
        }

        public IQueryable<Question> GetUsersQuestionsAskedWithAns(string id)
        {
            return _context.Questions.Include(q => q.WhoAsked)
                .Include(q => q.Answers)
                .Where(q => q.AskedToWhom.Any(u => u.Id == id));
        }

        public IQueryable<Question> GetUserFavoriteQuestionsWithAnsAskToWhomWhoAsk(string id)
        {
            return _context.Questions
                .Include(q => q.Answers)
                .Include(q => q.AskedToWhom)
                .Include(q => q.WhoAsked)
                .Where(q => q.UsersTrack.Any(u => u.Id == id));
        }

        public IEnumerable<Question> GetAllAnsweredQuestionsWithCtgWhoAskVtsAnsAskToWhom()
        {
            return _context.Questions
                .Include(q => q.Category)
                .Include(q => q.WhoAsked)
                .Include(q => q.Votes)
                .Include(q => q.Answers)
                .Include(q => q.AskedToWhom)
                .ToList().OrderByDescending(q => q.DateTime)
                .Where(q => q.AskedToWhom.Count != 0);
        }

        public void Add(Question question)
        {
            _context.Questions.Add(question);
        }

        public void Remove(Question question)
        {
            _context.Questions.Remove(question);
        }
    }
}