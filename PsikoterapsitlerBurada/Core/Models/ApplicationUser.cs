using System.Collections.Generic;
using System.Security.Claims;
using System.Threading.Tasks;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.EntityFramework;

namespace PsikoterapsitlerBurada.Core.Models
{
    // You can add profile data for the user by adding more properties to your ApplicationUser class, please visit https://go.microsoft.com/fwlink/?LinkID=317594 to learn more.
    public class ApplicationUser : IdentityUser
    {
        public ApplicationUser()
        {
            AskedQuestions = new HashSet<Question>();
            QuestionsAsked = new HashSet<Question>();
            LikesAnswers = new HashSet<Answer>();
            Answers = new HashSet<Answer>();
            Followees = new HashSet<Following>();
            Followers = new HashSet<Following>();
            UserNotifications = new HashSet<UserNotification>();
        }

        public virtual ICollection<Question> AskedQuestions { get; set; }
        public virtual ICollection<Question> QuestionsAsked { get; set; }
        public virtual ICollection<Answer> LikesAnswers { get; set; }
        public virtual ICollection<Answer> Answers { get; set; }
        public virtual ICollection<Question> FavoriteQuestions { get; set; }
        public virtual ICollection<Following> Followers { get; set; }
        public virtual ICollection<Following> Followees { get; set; }
        public virtual ICollection<UserNotification> UserNotifications { get; private set; }

        public async Task<ClaimsIdentity> GenerateUserIdentityAsync(UserManager<ApplicationUser> manager)
        {
            // Note the authenticationType must match the one defined in CookieAuthenticationOptions.AuthenticationType
            var userIdentity = await manager.CreateIdentityAsync(this, DefaultAuthenticationTypes.ApplicationCookie);
            // Add custom user claims here
            return userIdentity;
        }

        public void Notify(Notification notification)
        {
            var userNotification = new UserNotification()
            {
                UserId = Id,
                Notification = notification
            };

            UserNotifications.Add(userNotification);
        }
    }
}