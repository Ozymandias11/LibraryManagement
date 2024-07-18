using FluentResults;
using Library.Service.Dto.Library.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Interfaces
{
   public interface IReservationService
    {
        Task<IEnumerable<ReservationDto>> GetAllReservations(string sortBy, string sortOrder, string searchString, int page, int pageSize, bool trackChanges);
        Task<Result<ReservationDetailsDto>> GetReservation(Guid id, bool trackChanges);
        Task<Result> CreateReservation(CreateReservationDto createReservationDto);
        Task<(bool isAvailable, string message)> CheckBookCopyAvailability(Guid originalBookId, string edition, Guid PublisherId, int quantity);
        Task<Result> ReturnBook(ReturnBookDto returnBookDto);
    }
}
