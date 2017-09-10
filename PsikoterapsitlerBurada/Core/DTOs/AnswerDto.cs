using System.Collections.Generic;

namespace PsikoterapsitlerBurada.Core.DTOs
{
    public class AnswerDto
    {
        public int Id { get; set; }
        public string AnswerText { get; set; }
        public int QuestionId { get; set; }
        public ICollection<UserDto> Likes { get; set; }
        public UserDto User { get; set; }
    }
}