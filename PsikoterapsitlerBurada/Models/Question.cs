using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace PsikoterapsitlerBurada.Models
{
    public class Question
    {
        public Question()
        {
            AskedToWhom = new HashSet<ApplicationUser>();
            Votes = new HashSet<Vote>();
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
        public ICollection<Vote> Votes { get; set; }
    }
}