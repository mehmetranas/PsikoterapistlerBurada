using AutoMapper;
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
                .Include(a => a.Question)
                .Include(a => a.User)
                .Include(a => a.Likes)
                .Select(Mapper.Map<AnswerViewModel>)
                .ToList();
           
            var question = _context.Questions
                .Include(q => q.AskedToWhom)
                .Include(q => q.WhoAsked)
                .Select(Mapper.Map<QuestionViewModel>)
                .SingleOrDefault(q => q.Id == id);

            

            var viewModel = new GetAnswerViewModel()
            {
                QuestionViewModel = question,
                AnswerViewModels = answers
            };

            return View(viewModel);
        }
    }
}