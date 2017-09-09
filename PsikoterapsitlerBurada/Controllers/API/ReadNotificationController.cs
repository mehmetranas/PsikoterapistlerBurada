using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class ReadNotificationController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReadNotificationController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }

        //api/controler/notificationId

        [HttpPost]
        public IHttpActionResult Read(int id)
        {
            var userId = User.Identity.GetUserId();
            var usernotification =
                _unitOfWork.UserNotifications.GetUserNotificationByUserAndNotificationt(id, userId);

            usernotification?.Read();
            _unitOfWork.Complete();
            return Ok();
        }
    }
}
