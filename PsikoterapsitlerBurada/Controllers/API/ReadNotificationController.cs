using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Core.Repositories;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class ReadNotificationController : ApiController
    {
        private readonly IUnitOfWork _unitOfWork;

        public ReadNotificationController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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
