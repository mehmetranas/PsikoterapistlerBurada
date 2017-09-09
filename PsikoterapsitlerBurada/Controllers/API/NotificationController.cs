using AutoMapper;
using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.DTOs;
using System.Collections.Generic;
using System.Linq;
using System.Web.Http;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;

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
