using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using Microsoft.VisualBasic.ApplicationServices;
using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Repositories
{
    public class UserRepository
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

        public ApplicationUser GetUserForUserProfile(string id)
        {
            return _context.Users
                .Include(u => u.Followees)
                .Include(u => u.Followers)
                .Include(u => u.FavoriteQuestions)
                .Include(u => u.LikesAnswers)
                .SingleOrDefault(u => u.Id == id);
        }
    }
}