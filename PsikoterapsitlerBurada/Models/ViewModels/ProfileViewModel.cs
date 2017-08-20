using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PsikoterapsitlerBurada.Models.ViewModels
{
    public class ProfileViewModel
    {
        public ApplicationUser User { get; set; }
        public bool IsFollow { get; set; }
        public string Follow => "Takibi Bırak";
        public string UnFollow => "Takip Et";
        public string FollowState => IsFollow ? Follow : UnFollow;
    }
}