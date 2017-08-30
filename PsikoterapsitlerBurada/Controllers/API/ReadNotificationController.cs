using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Repositories;
using System.Web.Http;

namespace PsikoterapsitlerBurada.Controllers.API
{
    [Authorize]
    public class ReadNotificationController : ApiController
    {
        private readonly UserNotificationRepository _userNotificationRepository;
        private readonly UnitOfWork _unitOfWork;

        public ReadNotificationController()
        {
            var context = new ApplicationDbContext();
            _userNotificationRepository = new UserNotificationRepository(context);
            _unitOfWork = new UnitOfWork(context);
        }

        //api/controler/notificationId

        [HttpPost]
        public IHttpActionResult Read(int id)
        {
            var userId = User.Identity.GetUserId();
            var usernotification =
                _userNotificationRepository.GetUserNotificationByUserAndNotificationt(id, userId);

            usernotification?.Read();
            _unitOfWork.Complate();
            return Ok();
        }
    }
}
