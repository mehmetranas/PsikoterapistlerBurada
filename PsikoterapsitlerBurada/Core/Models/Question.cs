using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;

namespace PsikoterapsitlerBurada.Core.Models
{
    public class Question
    {
        public Question()
        {
            AskedToWhom = new HashSet<ApplicationUser>();
            Votes = new HashSet<Vote>();
            Answers = new HashSet<Answer>();
            UsersTrack = new HashSet<ApplicationUser>();
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
        public ICollection<Answer> Answers { get; set; }
        public ICollection<ApplicationUser> UsersTrack { get; set; }
        public int TotalVotes
        {
            get { return Votes.Sum(v => v.VoteState); }
        }
    }
}