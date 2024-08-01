using Library.Data.Extensions;
using Library.Data.Library.Interfaces;
using Library.Data.RequestFeatures;
using Library.Model.Enums;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Implementations
{
    public class BookCopyRepository : RepositoryBase<BookCopy>, IBookCopyRepository
    {
        public BookCopyRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void AddBookCopies(Guid originalBookId, Guid PublisherId, IEnumerable<BookCopy> bookCopies)
        {

            var modifiedBookCopies = new List<BookCopy>();

            foreach (var bookCopy in bookCopies)
            {
                bookCopy.PublisherId = PublisherId;
                bookCopy.OriginaBookId = originalBookId;
                bookCopy.CreatedDate = DateTime.Now;
                modifiedBookCopies.Add(bookCopy);
            }

           
            AddRange(modifiedBookCopies);

        }
       
        public void DeleteBookCopy(BookCopy bookCopy) => Delete(bookCopy);


        public async Task<IEnumerable<BookCopy>> GetAllAvailableBookCopies(Guid originalBookId, string edition, Guid publisherId, int quantity)
          => await FindByCondition(bc => bc.OriginaBookId == originalBookId &&
                                   bc.Edition == edition &&
                                   bc.PublisherId == publisherId &&
                                   bc.Status == Status.Available,
                                   trackChanges: false)
                                   .Take(quantity)
                                   .ToListAsync();

        public async Task<IEnumerable<BookCopy>> GetBookCopiesOfReservation(Guid ReservationId)
            => await FindByCondition(bc => bc.ReservationItems!.Any(ri => ri.ReservationId == ReservationId), false).ToListAsync();
        

        public Task<BookCopy?> GetBookCopy(Guid id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<BookCopy>> GetCustomerNumberOfCopies(Guid originalBookId, string edition, Guid publisherId, int quantity)
            => await FindByCondition(bc => bc.OriginaBookId == originalBookId &&
                                           bc.PublisherId == publisherId &&
                                           bc.Edition == edition,
                                           trackChanges: false)
                                        .Take(quantity).ToListAsync();
                                           
       

        public async Task<int> GetTotalBookCopiesCount()
        {
            var allBookCopies = await FindAll(false).ToListAsync();
            return allBookCopies
                .GroupBy(bc => new { bc.OriginaBookId, bc.PublisherId, bc.Edition })
                .Count();
        }

        public void UpdateBookCopyStatus(BookCopy bookCopy) => Update(bookCopy);

        public async Task<PagedList<BookCopy>> GetAllBookCopies(BookCopyParameters bookCopyParameters, bool trackChanges)
        {
            var query = FindAll(trackChanges)
                .Include(bc => bc.OriginalBook)
                .Include(bc => bc.Publisher)
                .Include(bc => bc.Shelves)
                      .ThenInclude(bcs => bcs.Shelf)
                        .ThenInclude(s => s.Room)
                .Search(bookCopyParameters.SearchTerm)
                .Sort(bookCopyParameters.OrderBy);

            // Get distinct combinations
            var distinctCombos = await query
                .Select(bc => new { bc.OriginaBookId, bc.PublisherId, bc.Edition })
                .Distinct()
                .ToListAsync();

            // Process each combination
            var results = new List<BookCopy>();
            foreach (var combo in distinctCombos)
            {
                var bookCopies = await query
                    .Where(bc => bc.OriginaBookId == combo.OriginaBookId &&
                                 bc.PublisherId == combo.PublisherId &&
                                 bc.Edition == combo.Edition &&
                                 bc.Status == Status.Available)
                    .ToListAsync();

                if (bookCopies.Any())
                {
                    var firstCopy = bookCopies.First();
                    firstCopy.Quantity = bookCopies.Count;
                    results.Add(firstCopy);
                }
            }


            return PagedList<BookCopy>.ToPagedList(results,
                bookCopyParameters.PageNumber,
                bookCopyParameters.PageSize);
        }

    }
}
