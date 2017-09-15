using AutoMapper;
using PsikoterapsitlerBurada.Core.Models.ViewModels;
using PsikoterapsitlerBurada.Core.Repositories;
using System.Linq;
using System.Web.Mvc;

namespace PsikoterapsitlerBurada.Controllers
{
    public class HomeController : Controller
    {
        private readonly IUnitOfWork _unitOfWork;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
        
        public ActionResult Index()
        {
            var questions =  _unitOfWork.Questions
                .GetAllAnsweredQuestionsWithCtgWhoAskVtsAnsAskToWhom()
                .Select(Mapper.Map<QuestionViewModel>);
                
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

        public ActionResult Search()
        {
            return View();
        }

        [HttpPost]
        public ActionResult GetSearchList(string query)
        {
            var questions = _unitOfWork.Questions.GetAllAnsweredQuestionsWithCtgWhoAskVtsAnsAskToWhom();

            var viewModel = questions
                .Where(q => q.QuestionText.ToLower().Contains(query.ToLower()))
                .Select(Mapper.Map<QuestionViewModel>);
            if (!viewModel.Any())
                return PartialView("_EmptySearchResult", "Aramanızla eşleşen bir soru bulamadık.");

            return PartialView("_GetQuestions", viewModel);
        }

       
    }
}