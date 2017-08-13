using PsikoterapsitlerBurada.Models;
using System.Linq;
using System.Web.Mvc;

namespace PsikoterapsitlerBurada.Controllers
{
    public class HomeController : Controller
    {
        private ApplicationDbContext _context;

        public HomeController()
        {
            _context = new ApplicationDbContext();
        }
        
        public ActionResult Index()
        {
            var questions = _context.Questions
                .Include("Category")
                .Include("WhoAsked")
                .Include("Votes")
                .Where(q => q.AskedToWhom.Count != 0)
                .ToList(); 

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
    }
}