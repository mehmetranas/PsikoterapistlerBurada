using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PsikoterapsitlerBurada.Models
{
    public class UserNotification
    {
        [Key]
        [Column(Order = 1)]
        public int NotificationId { get; set; }
        [Key]
        [Column(Order = 2)]
        public string UserId { get; set; }
        public Notification Notification { get; set; }
        public ApplicationUser User { get; set; }
        public bool IsRead { get; private set; }

        public void Read()
        {
            IsRead = true;
        }
    }
}