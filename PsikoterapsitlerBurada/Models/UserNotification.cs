using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PsikoterapsitlerBurada.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public string UserId { get; set; }
        [Key]
        [Column(Order = 2)]
        public int NotificationId { get; set; }
        public Notification Notification { get; set; }
        public ApplicationUser User { get; set; }
    }
}