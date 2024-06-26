using AutoMapper;
using Library.Data.NewFolder;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
using Library.Service.Library.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Library.Service.Library.Implementations
{
    public class BookCopyService : IBookCopyService
    {
        private readonly IRepositoryManager _repositoryManager;
        private readonly IMapper _mapper;
        public BookCopyService(IRepositoryManager repositoryManager, IMapper mapper)
        {
            _repositoryManager = repositoryManager;
            _mapper = mapper;
        }

        public async Task CreateBookCopy(
            Guid originalBookId,
            Guid PublisherId, 
            CreateBookCopyDto createBookCopyDto)
        {
            var BookCopies = Enumerable.Range(0, createBookCopyDto.Quantity)
                .Select(_ => new BookCopy
                {
                    NumberOfPages = createBookCopyDto.NumberOfPages,
                    Status = createBookCopyDto.Status,  
                    Edition = createBookCopyDto.Edition,    
                    Quantity = createBookCopyDto.Quantity,  
                });

            _repositoryManager.BookCopyRepository.AddBookCopies(originalBookId, PublisherId ,BookCopies);   

            

            await _repositoryManager.SaveAsync();
        }

        public async Task<IEnumerable<BookCopyDto>> GetAllBookCopies(int page, int pageSize,bool trackChanges)
        {
            var bookCopy = await _repositoryManager.BookCopyRepository.GetAllBookCopies(page, pageSize,trackChanges);
            var bookCopyDto = _mapper.Map<IEnumerable<BookCopyDto>>(bookCopy);  


            return bookCopyDto;
        }

        public async Task<int> GetTotalBookCopiesCount() => await _repositoryManager.BookCopyRepository.GetTotalBookCopiesCount();
       
    }
}
