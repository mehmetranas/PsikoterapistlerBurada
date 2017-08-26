using System;
using System.Collections.Generic;

namespace PsikoterapsitlerBurada.DTOs
{
    public class QuestionDto
    {
        public int Id { get; set; }
        public string QuestionText { get; set; }
        public UserDto WhoAsked { get; set; }
        public ICollection<UserDto> AskedToWhom { get; set; }
        public DateTime? DateTime { get; set; }
        public ICollection<AnswerDto> Answers { get; set; }
        public ICollection<UserDto> UsersTrack { get; set; }
    }
}