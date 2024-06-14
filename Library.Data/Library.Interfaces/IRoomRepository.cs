using Library.Model.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Data.Library.Interfaces
{
   public interface IRoomRepository
    {
        Task<IEnumerable<Room>> GetAllRooms(bool trackChanges);
    }
}
