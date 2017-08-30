using System.Collections.Generic;
using System.Linq;
using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Repositories
{
    public interface IQuestionRepository
    {
        Question GetQuestionByQuestionId(int id);
        IOrderedEnumerable<Question> GetAllQuestions();
        IOrderedQueryable<Question> GetUserQuestions(string userId);
        IQueryable<Question> GetUserUnAskedQuestions(string userId);
        IQueryable<Question> GetQuestionsAskedToUser(string userId);
        IQueryable<Question> GetUserQuestionsWithWhoAskAskToWhomAns(string id);
        IQueryable<Question> GetUsersQuestionsAskedWithAns(string id);
        IQueryable<Question> GetUserFavoriteQuestionsWithAnsAskToWhomWhoAsk(string id);
        IEnumerable<Question> GetAllAnsweredQuestionsWithCtgWhoAskVtsAnsAskToWhom();
        void Add(Question question);
        void Remove(Question question);
    }
}