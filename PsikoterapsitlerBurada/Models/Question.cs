using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace PsikoterapsitlerBurada.Models
{
    public class Question
    {
        public Question()
        {
            AskedToWhom = new HashSet<ApplicationUser>();
        }
        public int Id { get; set; }
        [Required]
        public string QuestionText { get; set; }
        [Required]
        public ApplicationUser WhoAsked { get; set; }
        public ICollection<ApplicationUser> AskedToWhom { get; set; }
        public DateTime? DateTime { get; set; }
        [Required]
        public Category Category { get; set; }
    }
}