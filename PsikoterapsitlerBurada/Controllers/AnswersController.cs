using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Models.ViewModels;
using System.Data.Entity;
using System.Linq;
using System.Web.Mvc;

namespace PsikoterapsitlerBurada.Controllers
{

    public class AnswersController : Controller
    {
        private ApplicationDbContext _context;

        public AnswersController()
        {
            _context = new ApplicationDbContext();
        }

        public ActionResult GetAnswers(int id)
        {
            var answers = _context.Answers.Where(a => a.Question.Id == id)
                .Select(a => new AnswerViewModel()
                {
                    Question = a.Question,
                    AnswerText = a.AnswerText,
                    User = a.User,
                    Likes = a.Likes,
                    DateTime = a.DateTime,
                    QuestionId = a.QuestionId,
                    Id = a.Id,
                    UserId = a.UserId
                })
                .ToList();

            var question = _context.Questions
                .Include(q => q.AskedToWhom)
                .Include(q => q.WhoAsked)
                .SingleOrDefault(q => q.Id == id);

          return View(answers);
        }
    }
}