using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;

namespace PsikoterapsitlerBurada.Core.Models.ViewModels
{
    public class QuestionViewModel
    {
        [DisplayName("Soru")]
        public string QuestionText { get; set; }
        public ApplicationUser WhoAsked { get; set; }
        [DisplayName("Kimlere Sormak İstiyorsun?")]
        public IEnumerable<ApplicationUser> AskedToWhom { get; set; }
        public DateTime? DateTime { get; set; }
        [DisplayName("Kategori")]
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
        public IEnumerable<Vote> Votes { get; set; }
        public int Id { get; set; }
        public ICollection<Answer> Answers { get; set; }

        public bool IsAskedToUser(string id)
        {
            return AskedToWhom.Any(u => u.Id == id);
        }

        public bool IsAnsweredByUser(string id)
        {
            return Answers.Any(u => u.UserId == id);
        }

        public int? TotalVotes
        {
            get { return Votes.Sum(v => v.VoteState); }
        }

    }
}