using System.Data.Entity;
using System.Linq;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;

namespace PsikoterapsitlerBurada.Persistence.Repositories
{

    public class FollowingRepository : IFollowingRepository
    {
        private readonly ApplicationDbContext _context;

        public FollowingRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IQueryable<Following> GetFollowingsByFollower(string id)
        {
            return _context.Followings
                .Include(f => f.Followee)
                .Include(f => f.Follower)
                .Where(f => f.FollowerId == id);
        }

        public IQueryable<Following> GetFollowingsByFollowee(string id)
        {
            return _context.Followings
                .Include(f => f.Followee)
                .Include(f => f.Follower)
                .Where(f => f.FolloweeId == id);
        }

        public void Add(Following following)
        {
            _context.Followings.Add(following);
        }

        public void Remove(Following following)
        {
            _context.Followings.Remove(following);
        }
    }
}