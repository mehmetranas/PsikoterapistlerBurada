using System.Linq;
using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Repositories
{
    public interface INotificationRepository
    {
        Notification GetNotificationByFollowing(int id);
        Notification GetNotificationByUserLikeAndAnswer(string userId, int answerId);
        Following GetFollowingsByFollowerAndFollowee(string id, string userId);
        IQueryable<Notification> GetUserNotiicationwithNotificationByUserId(string id);
        void Add(Notification notification);
        void Remove(Notification notification);
    }
}