using PsikoterapsitlerBurada.Models;
using System.Linq;
using System.Web.Mvc;

namespace PsikoterapsitlerBurada.Controllers
{
    public class QuestionController : Controller
    {
        private ApplicationDbContext _context;

        public QuestionController()
        {
            _context = new ApplicationDbContext();
        }

        // GET: Question
        [Authorize]
        public ActionResult Create()
        {
            QuestionViewModel viewModel = new QuestionViewModel()
            {
                Categories = _context.Categories.ToList(),
                AskedToWhom =
                    _context.Users.ToList() //There is a problem that get all users properties, only get username and Id
            };
            return View(viewModel);
        }

        public ActionResult GetMyQuestions()
        {
            throw new System.NotImplementedException();
        }

        public ActionResult GetUnAskedQuestions()
        {
            throw new System.NotImplementedException();
        }
    }
}