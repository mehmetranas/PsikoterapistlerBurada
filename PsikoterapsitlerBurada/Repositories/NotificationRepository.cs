using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Repositories
{
    public class NotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        public Notification GetNotificationByFollowing(int id)
        {
            return _context.Notifications
                .SingleOrDefault(n => n.FollowingId == id);
        }

        public Notification GetNotificationByUserLikeAndAnswer(string userId, int answerId)
        {
            return _context.Notifications.SingleOrDefault(n => n.UserLikeId == userId && n.AnswerId == answerId);
        }

        public Following GetFollowingsByFollowerAndFollowee(string id, string userId)
        {
            return _context.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);
        }

        public IQueryable<Notification> GetUserNotiicationwithNotificationByUserId(string id)
        {
            return _context.UserNotifications
                .Where(un => un.UserId == id && un.IsRead == false)
                .Select(un => un.Notification)
                .Include(un => un.Answer)
                .Include(un => un.Answer.User)
                .Include(un => un.UserLike)
                .Include(un => un.Question.WhoAsked)
                .Include(un => un.Following)
                .Include(f => f.Following.Follower)
                .Include(f => f.Following.Followee);
        }

        public void Add(Notification notification)
        {
            _context.Notifications.Add(notification);
        }

        public void Remove(Notification notification)
        {
            _context.Notifications.Remove(notification);
        }
    }
}