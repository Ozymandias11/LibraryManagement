using AutoMapper;
using FluentAssertions;
using Library.Data;
using Library.Data.Library.Interfaces;
using Library.Data.NewFolder;
using Library.Service.Library.Implementations;
using Library.Service.Library.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace UnitTests.AuthorTests
{
    public  class AuthorServiceTests
    {
        private readonly Mock<IRepositoryManager> _mockRepositoryManager;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AuthorService _authorService;

        public AuthorServiceTests()
        {
            _mockRepositoryManager = new Mock<IRepositoryManager>();   
            _mockMapper = new Mock<IMapper>();

            var mockAuhtorRepository = new Mock<IAuthorRepository>();
            _mockRepositoryManager.Setup(a => a.AuthorRepository).Returns(mockAuhtorRepository.Object);

            _authorService = new AuthorService(_mockRepositoryManager.Object, _mockMapper.Object);
        }

        [Fact]
        public async Task GetAllAuhtors_ShouldReturnError_WhenReposiotryFails()
        {
            _mockRepositoryManager.Setup(r => r.AuthorRepository.GetAllAuthor(It.IsAny<bool>())).ThrowsAsync(new Exception("Repository failed"));

            var result = await _authorService.GetAllAuthors(null, null, null, false);

            result.IsFailed.Should().BeTrue();
            result.Errors.Should().ContainSingle()
                .Which.Message.Should().Be("Repository failed");

        }


    }
}
