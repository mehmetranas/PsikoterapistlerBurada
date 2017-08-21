using System.Linq;

namespace PsikoterapsitlerBurada.Models.ViewModels
{
    public class ProfileViewModel
    {
        private string AuthUserId { get; }
        public ApplicationUser User { get; set; }
        public bool IsFollow => User.Followees.Any(f => f.FollowerId == AuthUserId);
        public string Follow => "Takibi Bırak";
        public string UnFollow => "Takip Et";
        public string FollowState => IsFollow ? Follow : UnFollow;
        public bool IsAuthUser => User.Id == AuthUserId;

        public ProfileViewModel()
        {
        }

        public ProfileViewModel(string authUserId, ApplicationUser user)
        {
            AuthUserId = authUserId;
            User = user;
        }
    }
}