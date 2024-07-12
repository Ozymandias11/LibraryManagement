using AutoMapper;
using FluentResults;
using Library.Data.NewFolder;
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


            //temp solution, will be changed
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

        public async Task<IEnumerable<ReservationDto>> GetAllReservations(bool trackChanges)
        {
            var reservations = await _repositoryManager.ReservationRepository.GetAllReservations(trackChanges);
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
                       BookTitle = group.First().BookCopy.OriginalBook.Title,
                       PublisherName = group.First().BookCopy.Publisher.PublisherName,
                       Quantity = group.Count(),
                       Edition = group.First().BookCopy.Edition,
                       ActualReturnDate = group.Any(ri => !ri.ActualReturnDate.HasValue)
                           ? null
                           : group.Max(ri => ri.ActualReturnDate)
                   })
                   .ToList();



            return reservationDto;

        }
    }
}
