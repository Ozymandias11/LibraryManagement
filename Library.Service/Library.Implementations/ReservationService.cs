using AutoMapper;
using FluentResults;
using Library.Data.NewFolder;
using Library.Model.Enums;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
using Library.Service.Errors.NotFoundError;
using Library.Service.Library.Interfaces;
using Library.Service.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Implementations
{
    public class ReservationService : IReservationService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        private readonly ILoggerManager _loggerManager;
        public ReservationService(IRepositoryManager repositoryManager, IMapper mapper, ILoggerManager loggerManager)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
            _loggerManager = loggerManager;
        }

        public async Task<(bool isAvailable, string message)> CheckBookCopyAvailability(Guid originalBookId, string edition, Guid PublisherId, int quantity)
        {
            var availableCopies = await _repositoryManager.BookCopyRepository.GetAllAvailableBookCopies(originalBookId, edition, PublisherId, quantity);

            if(availableCopies.Count() >= quantity)
            {
                return (true, $"{availableCopies.Count()} copies available");
            }
            else
            {
                return (false, $"only {availableCopies.Count()} copies available");
            }

        }

        public async Task<Result> CreateReservation(CreateReservationDto createReservationDto)
        {
            var reservation = _mapper.Map<Reservation>(createReservationDto);

            if (reservation.ReservationItems == null)
            {
                reservation.ReservationItems = new List<ReservationItem>();
            }

            foreach (var bookCopyRequest in createReservationDto.BookCopyReservations)
            {
                var availableBookCopies = await _repositoryManager.BookCopyRepository.GetAllAvailableBookCopies(
                             bookCopyRequest.OriginalBookId, 
                             bookCopyRequest.Edition,
                             bookCopyRequest.PublisherId,
                             bookCopyRequest.Quantity   
                    );

                if(availableBookCopies.Count() < bookCopyRequest.Quantity)
                {
                    return Result.Fail($"Not enough availabe copies for book {bookCopyRequest.OriginalBookId}");
                }

                foreach(var bookCopy in availableBookCopies)
                {
                    bookCopy.Status = Model.Enums.Status.CheckedOut;
                    reservation.ReservationItems.Add(new ReservationItem()
                    {
                        BookCopyID = bookCopy.BookCopyId,
                    });

                     _repositoryManager.BookCopyRepository.UpdateBookCopyStatus(bookCopy);
                }

            }

            _repositoryManager.ReservationRepository.CreateReservation(reservation);
            await _repositoryManager.SaveAsync();
            return Result.Ok(); 

        }

        public async Task<IEnumerable<ReservationDto>> GetAllReservations(
            string sortBy, string sortOrder, string searchString, int page, int pageSize, bool trackChanges)
        {
            var reservations = await _repositoryManager.ReservationRepository.GetAllReservations(page, pageSize, trackChanges);

            if(!string.IsNullOrEmpty(searchString))
            {
                reservations = reservations.Where(r => r.Customer.CustomerPersonalId.Contains(searchString) ||
                                                       r.Employee.Email.Contains(searchString));
            }

            reservations = ApplySorting(reservations, sortBy, sortOrder);

            var reservationsDto = _mapper.Map<IEnumerable<ReservationDto>>(reservations);   

            return reservationsDto;
        }


        public async Task<Result<ReservationDetailsDto>> GetReservation(Guid id, bool trackChanges)
        {
            var reservation = await _repositoryManager.ReservationRepository.GetReservation(id, trackChanges);

            if(reservation == null)
            {
                return Result.Fail(new NotFoundError("reservation not found", id));
            }

            var reservationDto = _mapper.Map<ReservationDetailsDto>(reservation);

            reservationDto.ReservationItems = reservation.ReservationItems
                   .GroupBy(ri => ri.BookCopy.OriginalBook.BookId)
                   .Select(group => new ReservationItemForDetailsDto
                   {
                       ReservationItemId = group.Key,
                       BookTitle = group.First().BookCopy.OriginalBook.Title,
                       PublisherName = group.First().BookCopy.Publisher.PublisherName,
                       Quantity = group.Count(),
                       Edition = group.First().BookCopy.Edition,
                       ActualReturnDate = group.Any(ri => !ri.ActualReturnDate.HasValue)
                           ? null
                           : group.Max(ri => ri.ActualReturnDate),
                       ReturnCustomerId = group.FirstOrDefault()?.ReturnCustomer?.CustomerPersonalId,
                       CustomerGuid = group.First()?.ReturnCustomer?.CustomerId

                   })
                   .ToList();



            return reservationDto;

        }

        public async Task<Result> ReturnBook(ReturnBookDto returnBookDto)
        {
            var reservation = await _repositoryManager.ReservationRepository.GetReservation(returnBookDto.ReservationId, true);
            

            foreach(var returnItem in returnBookDto.returnItems)
            {
                var status = GetStatusFromReturnAction(returnItem.ReturnStatus);
                var itemsToUpdate = reservation.ReservationItems.Where(ri => ri.BookCopy.Status == Status.CheckedOut)
                                    .Take(returnItem.Quantity)
                                    .ToList();


                foreach(var item in itemsToUpdate)
                {
                    item.BookCopy.Status = status;
                    item.ReturnCustomerId = returnBookDto.CustomerId;
                    item.ActualReturnDate = DateTime.Now;
                }


            }

            await _repositoryManager.SaveAsync();
            return Result.Ok();

        

        }

        private static Status GetStatusFromReturnAction(string returnStatus)
        {
            return returnStatus.ToLower() switch
            {
                "safe" => Status.Available,
                "damaged" => Status.Damaged,
                "lost" => Status.Lost,
                _ => throw new Exception("Invalid return status"),
            };
        }

        private static IEnumerable<Reservation> ApplySorting(IEnumerable<Reservation> reservations, string sortBy, string sortOrder)
        {
            return reservations = sortBy switch
            {
                "CustomerPersonalID" => sortOrder == "CustomerPersonalID_Asc" ? reservations.OrderBy(r => r.Customer.CustomerPersonalId)
                                                                                : reservations.OrderByDescending(r => r.Customer.CustomerPersonalId),
                "CheckoutTime" => sortOrder == "CheckoutTime_Asc" ? reservations.OrderBy(r => r.CheckoutTime)
                                                                                : reservations.OrderByDescending(r => r.CheckoutTime),
                "EmployeeEmail" => sortOrder == "EmployeeEmail_Asc" ? reservations.OrderBy(r => r.Employee.Email)
                                                                                : reservations.OrderByDescending(r => r.Employee.Email),
                _ => reservations.OrderBy(r => r.CreatedDate)



            };
        }
    }
}
