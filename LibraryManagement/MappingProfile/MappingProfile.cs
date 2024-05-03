﻿using AutoMapper;
using Library.Model.Models;
using Library.Service.Dto;
using LibraryManagement.ViewModels;

namespace LibraryManagement.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {

            CreateMap<RegisterViewModel, RegisterViewModelDto>();
            CreateMap<LoginViewModel, LoginViewModelDto>();
            CreateMap<ResetPasswordViewModel, ResetPasswordViewModelDto>();

        }
    }
}
