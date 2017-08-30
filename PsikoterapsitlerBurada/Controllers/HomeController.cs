using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Models.ViewModels;
using PsikoterapsitlerBurada.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace PsikoterapsitlerBurada.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
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