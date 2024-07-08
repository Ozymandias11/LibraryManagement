using Library.Data.Library.Interfaces;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Implementations
{
    public class BookRepository : RepositoryBase<Book>, IBookRepository
    {
        public BookRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateBook(Book book) => Create(book);
       

        public void DeleteBook(Book book) => Delete(book);
      

        public async Task<IEnumerable<Book>> GetAllBooks(bool trackChanges) =>
            await FindByCondition(b => b.DeletedDate == null, trackChanges).OrderBy(b => b.CreatedDate).ToListAsync();

            
        public async Task<Book?> GetBook(Guid id, bool trackChanges)
        {
            return await FindByCondition(b => b.BookId.Equals(id), trackChanges)
            .Include(b => b.Authors)
            .ThenInclude(ba => ba.Author)
            .Include(b => b.Publishers)
            .ThenInclude(bp => bp.Publisher)
            .Include(b => b.Categories)
            .ThenInclude(bc => bc.Category)
            .SingleOrDefaultAsync();
        }

  
       
    }
}
