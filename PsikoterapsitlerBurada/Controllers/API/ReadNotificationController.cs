using Microsoft.AspNet.Identity;
using System.Web.Http;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;

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
            _unitOfWork.Complate();
            return Ok();
        }
    }
}
