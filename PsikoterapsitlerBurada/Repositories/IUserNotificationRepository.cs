using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Repositories
{
    public interface IUserNotificationRepository
    {
        UserNotification GetUserNotificationByUserAndNotificationt(int id, string userId);
    }
}