using AutoMapper;
using PsikoterapsitlerBurada.Core.Models.ViewModels;
using PsikoterapsitlerBurada.Core.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace PsikoterapsitlerBurada.Controllers
{

    public class AnswersController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public AnswersController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
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