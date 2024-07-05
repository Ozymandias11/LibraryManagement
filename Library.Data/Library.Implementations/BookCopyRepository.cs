using Library.Data.Library.Interfaces;
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
                modifiedBookCopies.Add(bookCopy);
            }

           
            AddRange(modifiedBookCopies);

        }
       


        public void DeleteBookCopy(BookCopy bookCopy) => Delete(bookCopy);



        public async Task<IEnumerable<BookCopy>> GetAllBookCopies(int page, int pageSize, bool trackChanges)
        {
            // First, fetch all data without grouping
            var query = FindAll(trackChanges)
                .Include(bc => bc.OriginalBook)
                .Include(bc => bc.Publisher)
                .Include(bc => bc.Shelves)
                  .ThenInclude(bcs => bcs.Shelf)
                   .ThenInclude(s => s.Room);


            // Fetch all book copies
            var allBookCopies = await query.ToListAsync();

            // Perform grouping and pagination in memory
            var groupedBookCopies = allBookCopies
                .GroupBy(bc => new { bc.OriginaBookId, bc.PublisherId, bc.Edition })
                .Select(g => new BookCopy
                {
                    BookCopyId = g.First().BookCopyId,
                    OriginalBook = g.First().OriginalBook,
                    Publisher = g.First().Publisher,
                    Edition = g.Key.Edition,
                    NumberOfPages = g.First().NumberOfPages,
                    Status = g.First().Status,
                    Quantity = g.Count(),
                    Shelves = g.SelectMany(bc => bc.Shelves).ToList()
                })
                .OrderBy(bc => bc.OriginalBook.Title)
                .Skip((page - 1) * pageSize)
                .Take(pageSize)
                .ToList();

            return groupedBookCopies;
        }




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
    }
}
