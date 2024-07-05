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
    public class ShelfRepository : RepositoryBase<Shelf>, IShelfRepository
    {
        public ShelfRepository(RepositoryContext repositoryContext) : base(repositoryContext)
        {
        }

        public async Task<Shelf?> GetShelf(Guid roomId, Guid shelfId, bool trackChanges)
            => await FindByCondition(s => s.RoomId == roomId && s.ShelfId == shelfId, trackChanges).FirstOrDefaultAsync();
      

        public async Task<IEnumerable<Shelf>> GetShelves(Guid roomId, bool trackChanges) => 
            await FindByCondition(s => s.RoomId == roomId, trackChanges).ToListAsync();
            
       
    }
}
