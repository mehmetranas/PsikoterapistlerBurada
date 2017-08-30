using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Repositories;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [System.Web.Http.Authorize]
    public class FollowingController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork ;

        public FollowingController()
        {
            var context = new ApplicationDbContext();        
            _unitOfWork = new UnitOfWork(context);        
        }

        [System.Web.Http.HttpPost]
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

            _unitOfWork.Followings.Add(following);
            _unitOfWork.Complate();

            return Ok();
        }
    }
}
