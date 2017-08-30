using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PsikoterapsitlerBurada.Core.Models;
using PsikoterapsitlerBurada.Core.Models.ViewModels;
using PsikoterapsitlerBurada.DTOs;

namespace PsikoterapsitlerBurada.App_Start
{
    public class MappingProfile: Profile
    {
        public MappingProfile()
        {
            CreateMap<Question, QuestionViewModel>();
            CreateMap<Answer, AnswerViewModel>();
            CreateMap<Question, QuestionDto>();
            CreateMap<QuestionDto, Question>();
            CreateMap<Answer, AnswerDto>();
            CreateMap<AnswerDto, Answer>();
            CreateMap<Following, FollowingDto>();
            CreateMap<FollowingDto, Following>();
            CreateMap<ApplicationUser, UserDto>();
            CreateMap<UserDto, ApplicationUser>();
            CreateMap<Notification, NotificationDto>();
            CreateMap<NotificationDto, Notification>();
        }
    }
}