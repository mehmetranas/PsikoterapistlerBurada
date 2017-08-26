using AutoMapper;
using PsikoterapsitlerBurada.DTOs;
using PsikoterapsitlerBurada.Models;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web.Http;
using Microsoft.AspNet.Identity;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class NotificationController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public NotificationController()
        {
            _context = new ApplicationDbContext();
        }

        [HttpGet]
        public IEnumerable<NotificationDto> GetAllNotifications()
        {
            var id = User.Identity.GetUserId();
            var notifications = _context.UserNotifications
                .Where(un => un.UserId == id && un.IsRead == false)
                .Select(un => un.Notification)
                .Include(un => un.Answer)
                .Include(un => un.Answer.User)
                .Include(un => un.UserLike)
                .Include(un => un.Question.WhoAsked)
                .Include(un => un.Following)
                .Include(f => f.Following.Follower)
                .Include(f => f.Following.Followee)
                .Select(Mapper.Map<NotificationDto>);

            return notifications;
        }

    }
}
