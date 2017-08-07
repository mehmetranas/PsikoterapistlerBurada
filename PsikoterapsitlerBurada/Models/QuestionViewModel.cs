using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Web;

namespace PsikoterapsitlerBurada.Models
{
    public class QuestionViewModel
    {
        [DisplayName("Soru")]
        public string QuestionText { get; set; }
        public ApplicationUser WhoAsked { get; set; }
        [DisplayName("Kimlere Sormak İstiyorsun?")]
        public IEnumerable<ApplicationUser> AskedToWhom { get; set; }
        public DateTime? DateTime { get; set; }
        [DisplayName("Kategori")]
        public Category Category { get; set; }
        public IEnumerable<Category> Categories { get; set; }
    }
}