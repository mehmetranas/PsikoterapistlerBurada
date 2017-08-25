﻿namespace PsikoterapsitlerBurada.Models
{
    public class Following
    {
        public int Id { get; set; }
        public string FollowerId { get; set; }
        public string FolloweeId { get; set; }
        public ApplicationUser Follower { get; set; }
        public ApplicationUser Followee { get; set; }
    }
}