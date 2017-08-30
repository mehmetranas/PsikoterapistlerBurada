using PsikoterapsitlerBurada.Core.Models;

namespace PsikoterapsitlerBurada.Core.Repositories
{
    public interface IVoteRepository
    {
        int GetVotesByQuestion(int id);
        void Add(Vote vote);
    }
}