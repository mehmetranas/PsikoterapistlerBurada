using System.Linq;
using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Repositories
{
    public interface IFollowingRepository
    {
        IQueryable<Following> GetFollowingsByFollower(string id);
        IQueryable<Following> GetFollowingsByFollowee(string id);
        void Add(Following following);
        void Remove(Following following);
    }
}