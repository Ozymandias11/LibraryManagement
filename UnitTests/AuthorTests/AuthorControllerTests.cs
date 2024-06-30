using AutoMapper;
using FluentAssertions;
using FluentResults;
using Library.Service.Dto.Library.Dto;
using Library.Service.Interfaces;
using Library.Service.Library.Interfaces;
using Library.Service.Logging;
using LibraryManagement.Controllers;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit.Sdk;

namespace UnitTests.AuthorTests
{
    public class AuthorControllerTests
    {
        private readonly Mock<IServiceManager> _serviceManagerMock;
        private readonly Mock<IMapper> _mockMapper;
        private readonly AuthorController _authorController;
        private readonly Mock<ILoggerManager> _mockLoggerManager;

        public AuthorControllerTests()
        {
            _serviceManagerMock = new Mock<IServiceManager>();  
            _mockMapper = new Mock<IMapper>();  
            _mockLoggerManager = new Mock<ILoggerManager>();

            var mockAuthorService = new Mock<IAuthorService>();

            _serviceManagerMock.Setup(r => r.AuthorService).Returns(mockAuthorService.Object);  

            _authorController = new AuthorController(
                _serviceManagerMock.Object, 
                _mockMapper.Object,
                _mockLoggerManager.Object);


        }


        [Fact]
        public async Task Authors_ShouldReturnErrorView_WhenServiceFails()
        {
            var errorResult = Result.Fail<IEnumerable<AuthorDto>>("Service failed");

            _serviceManagerMock.Setup(r => r.AuthorService.GetAllAuthors(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<bool>()))
                .ReturnsAsync(errorResult);

            var result = await _authorController.Authors(null, null, null);

            result.Should().NotBeNull();

            // verify that the error was logged succesfully

            _mockLoggerManager.Verify(
                x => x.LogError(It.Is<string>(s => s.Contains("Service failed"))),
                Times.Once());


        }

    }
}
