using System;

namespace PsikoterapsitlerBurada.Core.Models
{
    public class Vote
    {
        public int Id { get; set; }
        public Question Question { get; set; }
        public ApplicationUser User { get; set; }
        public int QuestionId { get; set; }
        public string UserId { get; set; }
        public DateTime DateTime { get; set; }
        public int VoteState { get; set; }
    }
}