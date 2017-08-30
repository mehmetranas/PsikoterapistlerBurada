using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Repositories
{
    public interface IVoteRepository
    {
        int GetVotesByQuestion(int id);
        void Add(Vote vote);
    }
}