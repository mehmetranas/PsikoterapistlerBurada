using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Models.ViewModels;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;

namespace PsikoterapsitlerBurada.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
        }
        
        public ActionResult Index()
        {
            var questions =  _unitOfWork.Questions.GetAllAnsweredQuestionsWithCtgWhoAskVtsAnsAskToWhom();
                
            return View(questions);
        }
        
        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Search()
        {
            return View();
        }

        public ActionResult GetSearchList(string query)
        {
            var users =_unitOfWork.Users.GetUsersByQuery(query);

            var authUserId = User.Identity.GetUserId();

            var viewModel = users.Select(user => new ProfileViewModel(authUserId, user)).ToList();

            return PartialView("_UserProfiles", viewModel);
        }

       
    }
}