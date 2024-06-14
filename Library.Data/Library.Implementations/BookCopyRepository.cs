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

            // Add the modified book copies to the collection
            AddRange(modifiedBookCopies);

        }
       


        public void DeleteBookCopy(BookCopy bookCopy) => Delete(bookCopy);  
      

        public async Task<IEnumerable<BookCopy>> GetAllBookCopies(int page, int pageSize,bool trackChanges)
            => await FindAll(trackChanges)
            .Include(bc => bc.OriginalBook)
            .Include(bc => bc.Publisher)
            .OrderBy(bc => bc.CreatedDate)
            .Skip((page - 1) * pageSize)   
            .Take(pageSize)
            .ToListAsync();
        
            
        

        public Task<BookCopy?> GetBookCopy(Guid id, bool trackChanges)
        {
            throw new NotImplementedException();
        }

        public async Task<int> GetTotalBookCopiesCount()
        {
           return await FindAll(false).CountAsync();
        }
    }
}
