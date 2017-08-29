using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Models.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;
using PsikoterapsitlerBurada.Repositories;

namespace PsikoterapsitlerBurada.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;
        private QuestionRepository _questionRepository;

        public HomeController()
        {
            _context = new ApplicationDbContext();
            _questionRepository = new QuestionRepository(_context);
        }
        
        public ActionResult Index()
        {
            var questions =  _questionRepository.GetAllQuestions().Where(q => q.AskedToWhom.Count != 0);
                
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
            var users = _context.Users
                .Where(u => u.UserName.StartsWith(query)).ToList();

            var authUserId = User.Identity.GetUserId();

            var viewModel = users.Select(user => new ProfileViewModel(authUserId, user)).ToList();

            return PartialView("_UserProfiles", viewModel);
        }
    }
}