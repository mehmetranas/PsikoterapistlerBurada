using Microsoft.Build.Framework;

namespace PsikoterapsitlerBurada.DTOs
{
    public class AnswerDto
    {
        public int Id { get; set; }
        [Required]
        public string AnswerText { get; set; }
        [Required]
        public int QuestionId { get; set; }
    }
}