using System.Collections.Generic;

namespace PsikoterapsitlerBurada.Models.ViewModels
{
    public class AnswersViewModel
    {
        public Question Question { get; set; }
        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}