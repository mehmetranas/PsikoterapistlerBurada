using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Repositories;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [System.Web.Http.Authorize]
    public class FollowingController : ApiController
    {
        private readonly UserRepository _userRepositiory;
        private readonly NotificationRepository _notificationRepository;
        private readonly FollowingRepository _followingRepository;
        private readonly UnitOfWork _unitOfWork ;

        public FollowingController()
        {
            var context = new ApplicationDbContext();        
            _userRepositiory = new UserRepository(context);        
            _notificationRepository = new NotificationRepository(context);        
            _followingRepository = new FollowingRepository(context);        
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

            _followingRepository.Add(following);

            var followee = _userRepositiory.GetUserById(id);

            followee?.Notify(notification);

            _unitOfWork.Complate();
            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _notificationRepository.GetFollowingsByFollowerAndFollowee(id, userId);

            if (following == null) return BadRequest();

            var notification = _notificationRepository.GetNotificationByFollowing(following.Id);

            if (notification != null) _notificationRepository.Remove(notification);

            _followingRepository.Add(following);
            _unitOfWork.Complate();

            return Ok();
        }
    }
}
