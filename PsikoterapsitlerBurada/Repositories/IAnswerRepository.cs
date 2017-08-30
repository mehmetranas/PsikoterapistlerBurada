using System.Collections.Generic;
using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Repositories
{
    public interface IAnswerRepository
    {
        void Add(Answer answer);
        Answer GetAnswerById(int id);
        ICollection<Answer> GetAnswersByQuestion(int id);
        void Remove(Answer answer);
    }
}