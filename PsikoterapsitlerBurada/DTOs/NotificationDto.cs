using PsikoterapsitlerBurada.Core.Models;

namespace PsikoterapsitlerBurada.DTOs
{
    public class NotificationDto
    {
        public int Id { get; set; }
        public NotificationType NotificationType { get; set; }
        public int? QuestionId { get; set; }
        public int? AnswerId { get; set; }
        public int? FollowingId { get; set; }
        public string UserLikeId { get; set; }
        public UserDto UserLike { get; set; }
        public QuestionDto Question { get; set; }
        public AnswerDto Answer { get; set; }
        public FollowingDto Following { get; set; }
    }
}