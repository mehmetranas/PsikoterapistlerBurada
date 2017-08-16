using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsikoterapsitlerBurada.Models.ViewModels
{
    public class GetAnswerViewModel
    {
        public ICollection<AnswerViewModel> AnswerViewModels { get; set; }
        public QuestionViewModel QuestionViewModel { get; set; }
    }
}