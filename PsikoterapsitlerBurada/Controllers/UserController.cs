using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PsikoterapsitlerBurada.Controllers
{
    [Authorize]
    public class UserController : Controller
    {
        private ApplicationDbContext _context;

        public UserController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult GetMyQuestions()
        {
            var userId = User.Identity.GetUserId();
            var myQuestions = _context.Questions.Where(q => q.WhoAsked.Id == userId).Include("WhoAsked").Include("AskedToWhom").ToList();
            return View(myQuestions);
        }

        public ActionResult GetUnAskedQuestions()
        {
            var userId = User.Identity.GetUserId();
            var questions = _context.Questions.Where(q => q.WhoAsked.Id == userId && q.AskedToWhom.Count == 0);
            return View(questions);
        }

        public ActionResult GetQustionAskedToMe()
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.
                SingleOrDefault(u => u.Id == userId);

            var questions = _context.Questions.Include("WhoAsked").Where(q => q.AskedToWhom.Any(u => u.Id == userId)).ToList().OrderBy(d => d.DateTime);

            if (user == null) return HttpNotFound();

            return View(questions);
        }
    }
}