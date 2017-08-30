using AutoMapper;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Models.ViewModels;
using PsikoterapsitlerBurada.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace PsikoterapsitlerBurada.Controllers
{

    public class AnswersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswersController()
        {
            var context = new ApplicationDbContext();
            _unitOfWork = new UnitOfWork(context);
        }

        public ActionResult GetAnswers(int id)
        {
            var answers = _unitOfWork.Answers.GetAnswersByQuestion(id).Select(Mapper.Map<AnswerViewModel>);

            var question = Mapper.Map<QuestionViewModel>(_unitOfWork.Questions.GetQuestionByQuestionId(id));

            var viewModel = new GetAnswerViewModel()
            {
                QuestionViewModel = question,
                AnswerViewModels = answers
            };

            return View(viewModel);
        }
    }
    }