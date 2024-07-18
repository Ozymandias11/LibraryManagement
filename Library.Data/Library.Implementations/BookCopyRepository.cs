﻿using Library.Data.Library.Interfaces;
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

        public void AddBookCopies(
            Guid originalBookId, 
            Guid PublisherId, 
            IEnumerable<BookCopy> bookCopies)
        {

            var modifiedBookCopies = new List<BookCopy>();

            // Modify the properties of each book copy
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
                                   bc.Status == Model.Enums.Status.Available,
                                   trackChanges: false)
                                   .Take(quantity)
                                   .ToListAsync();

        public async Task<IEnumerable<BookCopy>> GetAllBookCopies(int page, int pageSize, bool trackChanges)
        {


            var query = FindAll(trackChanges)
                .Include(bc => bc.OriginalBook)
                .Include(bc => bc.Publisher)
                .Include(bc => bc.Shelves)
                      .ThenInclude(bcs => bcs.Shelf)
                        .ThenInclude(s => s.Room);

            // First, get the distinct combinations
            var distinctCombos = await query
                .Select(bc => new { bc.OriginaBookId, bc.PublisherId, bc.Edition })
                .Distinct()
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToListAsync();

            // Then, for each combination, get the first book copy and count
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

            return results.OrderBy(bc => bc.OriginalBook.Title);


        }

        public async Task<IEnumerable<BookCopy>> GetBookCopiesOfReservation(Guid ReservationId)
            => await FindByCondition(bc => bc.ReservationItems.Any(ri => ri.ReservationId == ReservationId), false).ToListAsync();
        

        public Task<BookCopy?> GetBookCopy(Guid id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalBookCopiesCount()
        {
            var allBookCopies = await FindAll(false).ToListAsync();
            return allBookCopies
                .GroupBy(bc => new { bc.OriginaBookId, bc.PublisherId, bc.Edition })
                .Count();
        }

        public void UpdateBookCopyStatus(BookCopy bookCopy) => Update(bookCopy);
      
    }
}
