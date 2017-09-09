using AutoMapper;
using System.Linq;
using System.Web.Mvc;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Models.ViewModels;
using PsikoterapsitlerBurada.Core.Repositories;
using PsikoterapsitlerBurada.Persistence.Models;
using PsikoterapsitlerBurada.Persistence.Repositories;

namespace PsikoterapsitlerBurada.Controllers
{

    public class AnswersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswersController()
        {
            _unitOfWork = new UnitOfWork(new ApplicationDbContext());
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