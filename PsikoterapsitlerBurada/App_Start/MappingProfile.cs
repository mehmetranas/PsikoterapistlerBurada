using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PsikoterapsitlerBurada.Models;
using PsikoterapsitlerBurada.Models.ViewModels;

namespace PsikoterapsitlerBurada.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Question, QuestionViewModel>();
            CreateMap<Answer, AnswerViewModel>();
        }
    }
}