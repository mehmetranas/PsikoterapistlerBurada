using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Repositories;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class ReadNotificationController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReadNotificationController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }

        //api/controler/notificationId

        [HttpPost]
        public IHttpActionResult Read(int id)
        {
            var userId = User.Identity.GetUserId();
            var usernotification =
                _unitOfWork.UserNotifications.GetUserNotificationByUserAndNotificationt(id, userId);

            usernotification?.Read();
            _unitOfWork.Complate();
            return Ok();
        }
    }
}
