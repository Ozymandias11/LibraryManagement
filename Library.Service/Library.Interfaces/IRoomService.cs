using Library.Service.Dto.Library.Dto;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Interfaces
{
    public interface IRoomService
    {
        Task<IEnumerable<RoomDto>> GetallRooms(bool trackChanges);
    }
}
