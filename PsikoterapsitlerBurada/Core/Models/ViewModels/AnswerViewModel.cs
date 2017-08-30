using System;
using System.Collections.Generic;

namespace PsikoterapsitlerBurada.Core.Models.ViewModels
{
    public class AnswerViewModel
    {
        public AnswerViewModel()
        {
            Likes = new HashSet<ApplicationUser>();
        }

        public int Id { get; set; }
        public string AnswerText { get; set; }
        public Question Question { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<ApplicationUser> Likes { get; set; }
        public DateTime DateTime { get; set; }
        public int QuestionId { get; set; }
        public string UserId { get; set; }
        public int TotalLike => Likes.Count;
    }
}