using System.Collections.Generic;

namespace PsikoterapsitlerBurada.Core.Models.ViewModels
{
    public class AnswersViewModelWillDelete
    {
        public Question Question { get; set; }
        public ICollection<AnswerViewModel> Answers { get; set; }
    }
}