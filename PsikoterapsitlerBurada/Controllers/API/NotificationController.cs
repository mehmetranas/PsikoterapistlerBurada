using AutoMapper;
using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Core.DTOs;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;
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
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
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
