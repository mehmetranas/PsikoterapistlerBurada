using System.Linq;
using PsikoterapsitlerBurada.Core.Models;

namespace PsikoterapsitlerBurada.Core.Repositories
{
    public interface IFollowingRepository
    {
        IQueryable<Following> GetFollowingsByFollower(string id);
        IQueryable<Following> GetFollowingsByFollowee(string id);
        void Add(Following following);
        void Remove(Following following);
    }
}