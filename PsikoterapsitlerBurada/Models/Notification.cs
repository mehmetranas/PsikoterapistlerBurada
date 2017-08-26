
﻿namespace PsikoterapsitlerBurada.Models
{
    public class Notification
    {
        public int Id { get; set; }
        public int? QuestionId { get; set; }
        public int?  AnswerId { get; set; }
        public int? FollowingId { get; set; }
        public string UserLikeId { get; set; }
        public ApplicationUser UserLike { get; set; }
        public Question Question { get; set; }
        public Answer Answer { get; set; }
        public Following Following { get; set; }
        public NotificationType NotificationType { get; set; }
    }
}