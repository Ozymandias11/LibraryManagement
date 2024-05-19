using AutoMapper;
using Library.Model.Models;
using Library.Service.Dto;
using LibraryManagement.ViewModels;

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

            //navigation Menu
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
           

            //Emails
            CreateMap<EmailTemplate, EmailtemplateDto>().ReverseMap();
            CreateMap<EmailtemplateDto, EmailTemplateViewModel>().ReverseMap();


           


         

        }
    }
}
