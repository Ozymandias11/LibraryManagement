using AutoMapper;
using Library.Data.NewFolder;
using Library.Service.Dto.Library.Dto;
using Library.Service.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Implementations
{
    public class RoomService : IRoomService
    {
        private readonly IRepositoryManager _repositoryManager; 
        private readonly IMapper _mapper;   
        public RoomService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;   
        }
        public async Task<IEnumerable<RoomDto>> GetallRooms(bool trackChanges)
        {
            var rooms = await _repositoryManager.RoomRepository.GetAllRooms(trackChanges);

            var roomsDto =  _mapper.Map<IEnumerable<RoomDto>>(rooms);  

            return roomsDto;    
        }
    }
}
