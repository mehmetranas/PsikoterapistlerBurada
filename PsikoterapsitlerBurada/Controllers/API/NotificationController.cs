using AutoMapper;
using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.DTOs;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Repositories;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class NotificationController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public NotificationController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }

        [HttpGet]
        public IEnumerable<NotificationDto> GetAllNotifications()
        {
            var id = User.Identity.GetUserId();
            var notifications = _unitOfWork.Notifications.GetUserNotiicationwithNotificationByUserId(id)
                .Select(Mapper.Map<NotificationDto>);

            return notifications;
        }

      }
}
