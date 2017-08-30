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
        private readonly AnswerRepository _answerRepository;
        private readonly QuestionRepository _questionRepository;

        public AnswersController()
        {
            var context = new ApplicationDbContext();
            _answerRepository = new AnswerRepository(context);
            _questionRepository = new QuestionRepository(context);
        }

        public ActionResult GetAnswers(int id)
        {
            var answers = _answerRepository.GetAnswersByQuestion(id).Select(Mapper.Map<AnswerViewModel>);

            var question = Mapper.Map<QuestionViewModel>(_questionRepository.GetQuestionByQuestionId(id));

            var viewModel = new GetAnswerViewModel()
            {
                QuestionViewModel = question,
                AnswerViewModels = answers
            };

            return View(viewModel);
        }
    }
    }