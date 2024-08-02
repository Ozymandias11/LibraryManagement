using AutoMapper;
using Library.Model.Enums;
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

            CreateMap<AuthorDto, Author>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<AuthorDto, AuthorViewModel>().ReverseMap();  
            CreateMap<CreateAuthorViewModel, CreateAuthorDto>();
            CreateMap<CreateAuthorDto, Author>();

            //publsihers

            CreateMap<Publisher, PublisherDto>();   

            CreateMap<PublisherDto, Publisher>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now));  

            CreateMap<PublisherDto, PublisherViewModel>().ReverseMap();
            CreateMap<CreatePublisherViewModel, CreatePublisherDto>();
            CreateMap<CreatePublisherDto, Publisher>();

            //books

            CreateMap<Book, BookDto>();



            CreateMap<BookDto, BookViewModel>();


            CreateMap<BookDto, Book>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<UpdateBookViewModel, BookDto>().ReverseMap();
            CreateMap<CreateBookViewModel, CreateBookDto>();
            CreateMap<CreateBookDto, Book>();


            //categoreis

            CreateMap<Category, CategoryDto>().ReverseMap();


            CreateMap<CategoryDto, CategoryViewModel>();

            CreateMap<CreateCategoryViewModel, CreateCategoryDto>();
            CreateMap<CreateCategoryDto, Category>();

            CreateMap<CategoryViewModel, CategoryDto>();


            //Rooms

            CreateMap<Room, RoomDto>();
            CreateMap<RoomDto, RoomViewModel>();    

            //Shelves

            CreateMap<Shelf, ShelfDto>();
            CreateMap<ShelfDto , ShelfViewModel>();


            //BookCopy


            CreateMap<BookCopy, BookCopyDto>()
                .ForMember(dest => dest.BookTitle, opt => opt.MapFrom(src => src.OriginalBook.Title))
                .ForMember(dest => dest.PublisherName, opt => opt.MapFrom(src => src.Publisher.PublisherName));
                
            CreateMap<BookCopyDto, BookCopyViewModel>();

            CreateMap<CreateBookCopyViewModel, CreateBookCopyDto>()
            .ForMember(dest => dest.NumberOfPages, opt => opt.MapFrom(src => src.NumberOfPages))
            .ForMember(dest => dest.Edition, opt => opt.MapFrom(src => src.Edition))
            .ForMember(dest => dest.Status, opt => opt.MapFrom(src => src.Status))
            .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.Quantity));

            //Customers

            CreateMap<Customer, CustomerDto>();
            CreateMap<CustomerDto, Customer>()
                .ForMember(dest => dest.UpdatedDate, opt => opt.MapFrom(src => DateTime.Now));

            CreateMap<CustomerDto, CustomerViewModel>()
           .ForMember(dest => dest.Address, opt => opt.MapFrom(src =>
               $"{src.Address.City}, {src.Address.Street}, {src.Address.ZipCode}"));

            CreateMap<CreateCustomerViewModel, CreateCustomerDto>();
            CreateMap<CreateCustomerDto, Customer>();
            CreateMap<UpdateCustomerViewModel,  CustomerDto>().ReverseMap();

            //Reservations

            CreateMap<Reservation, ReservationDto>()
                .ForMember(dest => dest.CustomerPersonalID, opt => opt.MapFrom(src => src.Customer.CustomerPersonalId))
                .ForMember(dest => dest.EmployeeEmail, opt => opt.MapFrom(src => src.Employee.Email));
            CreateMap<ReservationDto, ReservationViewModel>();
            CreateMap<ReservationItem, ReservationItemDto>();
            CreateMap<ReservationItemDto, ReservationItemViewModel>();

            CreateMap<CreateReservationViewModel, CreateReservationDto>();
            CreateMap<CreateReservationDto, Reservation>();
            CreateMap<BookDto, BookDropdownViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.BookId));

            CreateMap<BookCopyReservationRequestViewModel, BookCopyReservationRequest>();

            CreateMap<CustomerDto, CustomerDropDownViewModel>()
                .ForMember(dest => dest.Id, opt => opt.MapFrom(src => src.CustomerId));

            //Reservation Details

            CreateMap<Reservation, ReservationDetailsDto>()
                .ForMember(dest => dest.CustomerFullName, opt => opt.MapFrom(src => $"{src.Customer.FirstName}  {src.Customer.LastName}"))
                .ForMember(dest => dest.EmployeeFullName, opt => opt.MapFrom(src => $"{src.Employee.FirstName} {src.Employee.LastName}"))
                .ForMember(dest => dest.ReservationItems, opt => opt.Ignore());


            CreateMap<ReservationDetailsDto, ReservationDetailsViewModel>();
            CreateMap<ReservationItemForDetailsDto, ReservationItemForDetailsViewModel>();

            //return book

            CreateMap<ReturnBookViewModel, ReturnBookDto>();
            CreateMap<ReturnActionViewModel, ReturnActionDto>();

            //Book Copy Logs

            CreateMap<CreateBookCopyLogDto, BookCopyLog>();
            CreateMap<BookCopyLog, BookCopyDto>();
            CreateMap<ModifyBookCopiesDto, CreateBookCopyDto>()
                .ForMember(dest => dest.Status, opt => opt.MapFrom(src => Status.Available))
                .ForMember(dest => dest.Quantity, opt => opt.MapFrom(src => src.QuantityModified));

            CreateMap<BookCopyLog, BookCopyLogDto>();
            CreateMap<ModifyBookCopiesViewModel, ModifyBookCopiesDto>();
            CreateMap<BookCopyLogDto, BookCopyLogsViewModel>();



        }
    }
}
