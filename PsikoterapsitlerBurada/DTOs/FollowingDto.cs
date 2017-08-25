namespace PsikoterapsitlerBurada.DTOs
{
    public class FollowingDto
    {
        public int Id { get; set; }
        public string FollowerId { get; set; }
        public string FolloweeId { get; set; }
        public UserDto Follower { get; set; }
        public UserDto Followee { get; set; }

    }
}