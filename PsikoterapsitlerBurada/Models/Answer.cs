using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PsikoterapsitlerBurada.Models
{
    public class Answer
    {
        public Answer()
        {
            Likes = new HashSet<ApplicationUser>();
        }
        public int Id { get; set; }
        [StringLength(int.MaxValue)]
        public string AnswerText { get; set; }
        public Question Question { get; set; }
        public ApplicationUser User { get; set; }
        public ICollection<ApplicationUser> Likes { get; set; }
        public DateTime DateTime { get; set; }
        public int QuestionId { get; set; }
        public string UserId { get; set; }
    }
}