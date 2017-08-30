using PsikoterapsitlerBurada.Models;
using System.Linq;

namespace PsikoterapsitlerBurada.Repositories
{
    public class UserNotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public UserNotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }
        public UserNotification GetUserNotificationByUserAndNotificationt(int id, string userId)
        {
            return _context.UserNotifications.SingleOrDefault(un => un.NotificationId == id && un.UserId == userId);
        }
    }
}