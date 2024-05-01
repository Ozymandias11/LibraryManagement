using AutoMapper;
using Library.Model.Models;
using Shared.ViewModels;

namespace LibraryManagement.MappingProfile
{
    public class MappingProfile : Profile
    {
        public MappingProfile()
        {
            CreateMap<RegisterViewModel, Employee>();
        }
    }
}
