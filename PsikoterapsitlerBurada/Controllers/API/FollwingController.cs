using System.Linq;
using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class FollowingController : ApiController
    {
        private ApplicationDbContext _context;

        public FollowingController()
        {
            _context = new ApplicationDbContext();        
        }

        [HttpPost]
        public IHttpActionResult Follow(string id)
        {
            var userId = User.Identity.GetUserId();

            var following = new Following()
            {
                FolloweeId = id,
                FollowerId = userId
            };

            _context.Followings.Add(following);

            _context.SaveChanges();

            return Ok();
        }

        [HttpDelete]
        public IHttpActionResult UnFollow(string id)
        {
            var userId = User.Identity.GetUserId();
            var following = _context.Followings.SingleOrDefault(f => f.FollowerId == userId && f.FolloweeId == id);
            if (following == null) return BadRequest();

            _context.Followings.Remove(following);
            _context.SaveChanges();

            return Ok();

        }
    }
}
