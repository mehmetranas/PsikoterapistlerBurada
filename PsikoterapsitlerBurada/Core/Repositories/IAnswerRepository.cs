using System.Collections.Generic;
using PsikoterapsitlerBurada.Core.Models;

namespace PsikoterapsitlerBurada.Core.Repositories
{
    public interface IAnswerRepository
    {
        void Add(Answer answer);
        Answer GetAnswerById(int id);
        ICollection<Answer> GetAnswersByQuestion(int id);
        void Remove(Answer answer);
    }
}