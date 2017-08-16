using PsikoterapsitlerBurada.Models;
using System.Data.Entity;
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
                .Where(q => q.AskedToWhom.Count != 0)
                .Include(q => q.Category)
                .Include(q => q.WhoAsked)
                .Include(q => q.Votes)
                .Include(q => q.Answers)
                .Include(q => q.AskedToWhom)
                .ToList().OrderByDescending(q => q.DateTime); 

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