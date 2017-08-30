using System.Collections.Generic;

namespace PsikoterapsitlerBurada.Core.Models.ViewModels
{
    public class SelectUserToAskQuestionViewModel
    {
        public QuestionViewModel Question { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}