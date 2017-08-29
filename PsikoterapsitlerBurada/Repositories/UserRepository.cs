﻿using System;
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
    }
}