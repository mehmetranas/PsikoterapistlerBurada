using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class ReadNotificationController : ApiController
    {
        private readonly ApplicationDbContext _context;

        public ReadNotificationController()
        {
            _context = new ApplicationDbContext();
        }

        //api/controler/notificationId

        [HttpPost]
        public IHttpActionResult Read(int id)
        {
            var userId = User.Identity.GetUserId();
            var usernotification =
                _context.UserNotifications.SingleOrDefault(un => un.NotificationId == id && un.UserId == userId);

            usernotification?.Read();
            _context.SaveChanges();
            return Ok();
        }
    }
}
