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

            CreateMap<RegisterViewModel, RegisterViewModelDto>();
            CreateMap<RegisterViewModelDto, Employee>();

            CreateMap<LoginViewModel, LoginViewModelDto>();

            CreateMap<ResetPasswordViewModel, ResetPasswordViewModelDto>();

            CreateMap<NavigationMenu, NavigationMenuDto>();
             

            CreateMap<NavigationMenuDto, NavigationMenuViewModel>();

            CreateMap<Employee, UserViewModelDto>();

            CreateMap<UserViewModelDto, UserVeiwModel>();

            CreateMap<EmailTemplate, EmailtemplateDto>().ReverseMap();

            CreateMap<EmailtemplateDto, EmailTemplateViewModel>().ReverseMap();

            CreateMap<ForgotPasswordViewModel, ForgotPasswordDto>();

            CreateMap<CreateEmployeeViewModel, CreateEmployeeViewModelDto>();

            CreateMap<Employee, UserForPendingViewModelDto>();
            CreateMap<UserForPendingViewModelDto, UserForPendingViewModel>();

        }
    }
}
