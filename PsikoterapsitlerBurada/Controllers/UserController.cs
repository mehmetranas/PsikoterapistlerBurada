using Microsoft.AspNet.Identity;
using PsikoterapsitlerBurada.Models;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PsikoterapsitlerBurada.Controllers
{
    public class UserController : Controller
    {
        private ApplicationDbContext _context;

        public UserController()
        {
            _context = new ApplicationDbContext();
        }

        [Authorize]
        public ActionResult GetMyQuestions()
        {
            var userId = User.Identity.GetUserId();
            var myQuestions = _context.Questions.Where(q => q.WhoAsked.Id == userId).Include("WhoAsked").Include("AskedToWhom").ToList();
            return View(myQuestions);
        }

        [Authorize]
        public ActionResult GetUnAskedQuestions()
        {
            var userId = User.Identity.GetUserId();
            var questions = _context.Questions.Where(q => q.WhoAsked.Id == userId && q.AskedToWhom.Count == 0);
            return View(questions);
        }

        [Authorize]
        public ActionResult GetQustionAskedToMe()
        {
            var userId = User.Identity.GetUserId();
            var user = _context.Users.SingleOrDefault(u => u.Id == userId);

            if (user == null) return HttpNotFound();

            var questions = user.QuestionsAsked;
            
            return View(questions);
        }
    }
}