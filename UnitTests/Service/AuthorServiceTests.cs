using AutoMapper;
using Bogus;
using FluentAssertions;
using Library.Data.NewFolder;
using Library.Model.Models;
using Library.Service.Dto.Library.Dto;
using Library.Service.Errors.NotFoundError;
using Library.Service.Library.Implementations;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.TestData;

namespace UnitTests.Service
{
   public class AuthorServiceTests
    {
        private readonly Mock<IRepositoryManager> _mockRepo;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AuthorService _service;
        private readonly Faker<Author> _authorFaker;

        public AuthorServiceTests()
        {
            _mockRepo = new Mock<IRepositoryManager>();
            _mockMapper = new Mock<IMapper>();
            _service = new AuthorService(_mockRepo.Object, _mockMapper.Object);
            _authorFaker = AuthorFaker.Create();
        }

        [Fact]
        public async Task DeleteAuthor_WithExistingId_ReturnsOkResult()
        {
            var author = _authorFaker.Generate();

            _mockRepo.Setup(repo => repo.AuthorRepository.GetAuthor(author.AuthorId, false))
                .ReturnsAsync(author);


            var result = await _service.DeleteAuthor(author.AuthorId, false);


            result.IsSuccess.Should().BeTrue();
            _mockRepo.Verify(r => r.AuthorRepository.DeleteAuthor(author), Times.Once());
            _mockRepo.Verify(r => r.SaveAsync(), Times.Once());



        }
        [Fact]
        public async Task DeleteAuthor_withNonExistingId_ReturnsFailResult()
        {
            var authorId = Guid.NewGuid();
            _mockRepo.Setup(repo => repo.AuthorRepository.GetAuthor(authorId, false))
                .ReturnsAsync((Author)null);

            var result = await _service.DeleteAuthor(authorId, false);


            result.IsFailed.Should().BeTrue();
            _mockRepo.Verify(r => r.AuthorRepository.DeleteAuthor(It.IsAny<Author>()), Times.Never);
            _mockRepo.Verify(r => r.SaveAsync(), Times.Never());    

        }
        [Fact]
        public async Task GetAuthor_WithExistingId_ReturnsAuthorDto()
        {
            var author = _authorFaker.Generate();

            var authorDto = new AuthorDto(author.AuthorId, author.FirstName, author.LastName, author.DateOfBirth);

            _mockRepo.Setup(r => r.AuthorRepository.GetAuthor(author.AuthorId, false))
                .ReturnsAsync(author);
            _mockMapper.Setup(m => m.Map<AuthorDto>(author))
                .Returns(authorDto);

            var result = await _service.GetAuthor(author.AuthorId, false);

            result.IsSuccess.Should().BeTrue();
            result.Value.Should().BeEquivalentTo(authorDto);



           

        }

        [Fact]
        public async Task GetAuthor_WithNonExistingId_ReturnsFailResult()
        {
            var nonExistentId = Guid.NewGuid();
            _mockRepo.Setup(r => r.AuthorRepository.GetAuthor(nonExistentId, false))
                .ReturnsAsync((Author)null);

            var result = await _service.GetAuthor(nonExistentId, false);

            result.IsFailed.Should().BeTrue();

            result.Errors.Should().ContainSingle()
                  .Which.Should().BeOfType<NotFoundError>()
                  .Which.Message.Should().Contain("Author")
                  .And.Contain(nonExistentId.ToString());

            _mockMapper.Verify(m => m.Map<AuthorDto>(It.IsAny<Author>()), Times.Never);


        }

        [Fact]
        public async Task UpdateAuthor_WithNonExistingAuthor_ReturnsFailResult()
        {

            var authorDto = new AuthorDto(Guid.NewGuid(), "John", "Doe", DateTime.Now);

            _mockRepo.Setup(r => r.AuthorRepository.GetAuthor(authorDto.AuthorId, true))
                          .ReturnsAsync((Author)null);

            var result = await _service.UpdateAuthor(authorDto, true);


            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle().Which.Message.Should().Contain("Author");
            _mockRepo.Verify(r => r.SaveAsync(), Times.Never);



        }


    }
}
