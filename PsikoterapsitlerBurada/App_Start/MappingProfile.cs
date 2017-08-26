using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using AutoMapper;
using PsikoterapsitlerBurada.DTOs;
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