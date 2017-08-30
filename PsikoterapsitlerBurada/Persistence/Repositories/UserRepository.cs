using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;

namespace PsikoterapsitlerBurada.Persistence.Repositories
{
    public class UserRepository : IUserRepository
    {
        private readonly ApplicationDbContext _context;

        public UserRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public IEnumerable<ApplicationUser> GetAllUsers()
        {
            return _context.Users.ToList();
        }

        public ApplicationUser GetUserById(string userId)
        {
            return _context.Users.SingleOrDefault(u => u.Id == userId);
        }

        public ApplicationUser GetUsers(string id)
        {
            return _context.Users
                .Include(u => u.Followees)
                .Include(u => u.Followers)
                .Include(u => u.FavoriteQuestions)
                .Include(u => u.LikesAnswers)
                .SingleOrDefault(u => u.Id == id);
        }

        public List<ApplicationUser> GetUsersByQuery(string query)
        {
            return _context.Users
                .Where(u => u.UserName.StartsWith(query)).ToList();
        }

        public IEnumerable<ApplicationUser> GetAllUsersWithOutAuthUser(string userId)
        {
            return _context.Users.ToList().Where(u => u.Id != userId);
        }

        public IEnumerable<int> GetUserLikeAnswersIdByQuestionId(ApplicationUser user, int questionId)
        {
            return user.LikesAnswers
                .Where(a => a.QuestionId == questionId)
                .Select(a => a.Id);
        }

        public ICollection<Question> GetUserFavoriteQuestions(string userId)
        {
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            return user?.FavoriteQuestions.ToList();
        }

        public void Add(ApplicationUser user)
        {
            _context.Users.Add(user);
        }

        public void Remove(ApplicationUser user)
        {
            _context.Users.Remove(user);
        }
    }
}