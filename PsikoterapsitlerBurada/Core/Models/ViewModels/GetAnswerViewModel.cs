using System.Collections.Generic;

namespace PsikoterapsitlerBurada.Core.Models.ViewModels
{
    public class GetAnswerViewModel
    {
        public IEnumerable<AnswerViewModel> AnswerViewModels { get; set; }
        public QuestionViewModel QuestionViewModel { get; set; }
    }
}