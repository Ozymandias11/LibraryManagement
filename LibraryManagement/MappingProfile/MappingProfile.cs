﻿using AutoMapper;
using Library.Model.Models;
using Library.Service.Dto;
using Library.Service.Dto.Library.Dto;
using LibraryManagement.ViewModels;
using LibraryManagement.ViewModels.Library.ViewModels;

namespace LibraryManagement.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            // Registration 
            CreateMap<RegisterViewModel, RegisterViewModelDto>();
            CreateMap<RegisterViewModelDto, Employee>();

            // Login
            CreateMap<LoginViewModel, LoginViewModelDto>();

            //Pasword Reset
            CreateMap<ResetPasswordViewModel, ResetPasswordViewModelDto>();
            CreateMap<ForgotPasswordViewModel, ForgotPasswordDto>();

            //Navigation Menu
            CreateMap<NavigationMenu, NavigationMenuDto>();
            CreateMap<NavigationMenuDto, NavigationMenuViewModel>();

            //Employee
            CreateMap<Employee, UserViewModelDto>();
            CreateMap<UserViewModelDto, UserVeiwModel>().ReverseMap();
            CreateMap<CreateEmployeeViewModel, CreateEmployeeViewModelDto>();
            CreateMap<Employee, UserForPendingViewModelDto>();
            CreateMap<UserForPendingViewModelDto, UserForPendingViewModel>();
            CreateMap<CreateAdminViewModel, CreateAdminViewModelDto>();
            CreateMap<CreateAdminViewModelDto, Employee>();
            CreateMap<AssignRoleViewModel, AssignRoleViewModelDto>();
            CreateMap<UserViewModelProfile , UserViewModelProfileDto>();
            CreateMap<UserViewModelProfileDto, Employee>();
            CreateMap<UserVeiwModel, UserViewModelDto>();
            CreateMap<UserViewModelDto,Employee>();


            //Emails
            CreateMap<EmailTemplate, EmailtemplateDto>().ReverseMap()
                .ForMember(dest => dest.From, opt => opt.Ignore())
                .ForMember(dest => dest.To, opt => opt.Ignore())
                .ForMember(dest => dest.TemplateName, opt => opt.Ignore());

            CreateMap<EmailtemplateDto, EmailTemplateViewModel>().ReverseMap()
                  .ForMember(dest => dest.From, opt => opt.Ignore())
                  .ForMember(dest => dest.To, opt => opt.Ignore());

            // Authors
            CreateMap<Author, AuthorDto>();
            CreateMap<AuthorDto, AuthorViewModel>();  
            CreateMap<CreateAuthorViewModel, CreateAuthorDto>();
            CreateMap<CreateAuthorDto, Author>();






        }
    }
}
