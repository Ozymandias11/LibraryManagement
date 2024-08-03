using Library.Data.RequestFeatures;
using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
    public interface IBookCopyRepository
    {
        Task<PagedList<BookCopy>> GetAllBookCopies(BookCopyParameters bookCopyParameters, bool trackChanges);
        Task<BookCopy?> GetBookCopy(Guid id, bool trackChanges);
        Task<IEnumerable<BookCopy>> GetBookCopiesOfReservation(Guid ReservationId);
        Task<IEnumerable<BookCopy>> GetAllAvailableBookCopies(Guid originalBookId, string edition, Guid publisherId, int quantity);
        Task<IEnumerable<BookCopy>> GetCustomNumberOfCopies(Guid originalBookId, string edition, Guid publisherId, int quantity);
        Task<int> GetTotalBookCopiesCount();
        void UpdateBookCopyStatus(BookCopy bookCopy);
        void DeleteBookCopy(BookCopy bookCopy);
        void AddBookCopies(Guid bookShelfId,Guid originalBookId, Guid PublisherId, IEnumerable<BookCopy> bookCopies);



    }
}
