using System.Linq;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;

namespace PsikoterapsitlerBurada.Persistence.Repositories
{
    public class UserNotificationRepository : IUserNotificationRepository
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