using Microsoft.AspNet.Identity;
using System.Web.Http;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class FollowingController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork ;

        public FollowingController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());        
        }

        [HttpPost]
        public IHttpActionResult Follow(string id)
        {
            var following = new Following()
            {
                FolloweeId = id,
                FollowerId = User.Identity.GetUserId()
            };

            var notification = new Notification()
            {
                Following = following,
                NotificationType = NotificationType.Follow
            };

            _unitOfWork.Followings.Add(following);

            var followee = _unitOfWork.Users.GetUserById(id);

            followee?.Notify(notification);

            _unitOfWork.Complate();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _unitOfWork.Notifications.GetFollowingsByFollowerAndFollowee(id, userId);

            if (following == null) return BadRequest();

            var notification = _unitOfWork.Notifications.GetNotificationByFollowing(following.Id);

            if (notification != null) _unitOfWork.Notifications.Remove(notification);

            _unitOfWork.Followings.Remove(following);
            _unitOfWork.Complate();

            return Ok();
        }
    }
}
