using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [System.Web.Http.Authorize]
    public class FollowingController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingController()
        {
            _context = new ApplicationDbContext();        
        }

        [System.Web.Http.HttpPost]
        public IHttpActionResult Follow(string id)
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            var following = new Following()
            {
                FolloweeId = id,
                FollowerId = userId
            };

            var notification = new Notification()
            {
                Following = following,
                NotificationType = NotificationType.Follow
            };

            _context.Followings.Add(following);

            user.Notify(notification);

            _context.SaveChanges();

            return Ok();
        }

        [System.Web.Http.HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = _context.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);

            if (following == null) return BadRequest();

            var notification = _context.Notifications.SingleOrDefault(n => n.FollowingId == following.Id);

            if (notification != null) _context.Notifications.Remove(notification);

            _context.Followings.Remove(following);

            _context.SaveChanges();

            return Ok();
        }
    }
}
