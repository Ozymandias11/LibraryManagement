using Library.Data.Library.Interfaces;
using Library.Data.NewFolder;
using Library.Model.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Implementations
{
    public class AuthorRepository : RepositoryBase<Author>, IAuthorRepository
    {
        public AuthorRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public void CreateAuthor(Author author) => Create(author);


        public void DeleteAuthor(Author author) => Delete(author);


        public async Task<IEnumerable<Author>> GetAllAuthor(bool trackChanges)
            => await FindAll(trackChanges).OrderBy(a => a.CreatedDate).ToListAsync();

        public async Task<Author?> GetAuthor(Guid id, bool trackChanges)
            => await FindByCondition(a => a.AuthorId == id, trackChanges).FirstOrDefaultAsync();

        public async Task<IEnumerable<Author>> GetAuthorsById(IEnumerable<Guid> ids, bool trackChanges) =>
            await FindByCondition(a => ids.Contains(a.AuthorId), trackChanges).ToListAsync();
        
            
        

        public async Task<IEnumerable<Author>> GetAuthorsOfBook(Guid id, bool trackChanges) =>
            await FindByCondition(a => a.BooksWritten.Any(ba => ba.BookId == id), trackChanges).ToListAsync();
        
            
        
    }
}
