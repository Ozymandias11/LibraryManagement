using AutoMapper;
using Library.Data;
using Library.Data.NewFolder;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using Library.Service.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Implementations
{
    public class ShelfService : IShelfService
    {

        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public ShelfService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ShelfDto>> GetShelves(Guid roomId, bool trackChanges)
        {
            //I will check for room existance


            var shelves = await _repositoryManager.ShelfRepository.GetShelves(roomId, trackChanges);

            var shelvesDto = _mapper.Map<IEnumerable<ShelfDto>>(shelves);   

            return shelvesDto;


        }
    }
}
