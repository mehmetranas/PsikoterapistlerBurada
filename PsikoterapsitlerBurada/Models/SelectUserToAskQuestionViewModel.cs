using System.Collections.Generic;

namespace PsikoterapsitlerBurada.Models
{
    public class SelectUserToAskQuestionViewModel
    {
        public Question Question { get; set; }
        public IEnumerable<ApplicationUser> Users { get; set; }
    }
}