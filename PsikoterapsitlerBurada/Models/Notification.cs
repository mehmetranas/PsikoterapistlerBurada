using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsikoterapsitlerBurada.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public NotificationType Type { get; set; }
        public Following Following { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
    }
}

