using PsikoterapsitlerBurada.Core.Models;

namespace PsikoterapsitlerBurada.Core.Repositories
{
    public interface IUserNotificationRepository
    {
        UserNotification GetUserNotificationByUserAndNotificationt(int id, string userId);
    }
}