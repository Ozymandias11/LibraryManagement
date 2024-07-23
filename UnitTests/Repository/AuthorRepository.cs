using Library.Data.Library.Interfaces;
using Library.Model.Models;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.TestData;

namespace UnitTests.Repository
{
    public class AuthorRepository
    {
        [Fact]
        public async Task GetAllAuthor_ReturnsAllNonDeletedAuthors()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            var authors = AuthorFaker.Create().Generate(3);

            mockRepo.Setup(repo => (repo.GetAllAuthor(false)))
                 .Returns(Task.FromResult(authors.AsEnumerable()));

            var result = await mockRepo.Object.GetAllAuthor(false);

            Assert.IsType<List<Author>>(result);
            Assert.Equal(3, result.Count());
            Assert.All(result, author => Assert.Null(author.DeletedDate));

        }
        [Fact]
        public async Task GetAuthor_ReturnsNull_WhenAuthorNotFound()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            var nonExistentId = Guid.NewGuid();
            mockRepo.Setup(repo => repo.GetAuthor(nonExistentId, It.IsAny<bool>()))
                .ReturnsAsync((Author?)null);

            var result = await mockRepo.Object.GetAuthor(nonExistentId, false);

            Assert.Null(result);
        }
        [Fact]
        public async Task GetAuthor_ReturnsCorrectAuthor()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            var author = AuthorFaker.Create().Generate();

            mockRepo.Setup(repo => repo.GetAuthor(author.AuthorId, It.IsAny<bool>()))
                .ReturnsAsync(author);

            var result = await mockRepo.Object.GetAuthor(author.AuthorId, false);

            Assert.NotNull(result);
            Assert.Equal(author.AuthorId, result.AuthorId);
        }
        [Fact]
        public async Task GetAuthorsOfBook_ReturnsEmptyList_WhenNoAuthorsForBookFound()
        {
            var mockRepo = new Mock<IAuthorRepository>();
            var bookId = Guid.NewGuid();

            mockRepo.Setup(repo => repo.GetAuthorsOfBook(bookId, It.IsAny<bool>()))
                .ReturnsAsync(new List<Author>());

            var result = await mockRepo.Object.GetAuthorsOfBook(bookId, false);


            Assert.Empty(result);



        }

        [Fact]
        public async Task GetAuthorsById_ReturnsCorrectAuthors()
        {
            var mockRepo = new Mock<IAuthorRepository>();  
            var authors = AuthorFaker.Create().Generate(3);
            
            var ids = authors.Select(a => a.AuthorId).ToList();

            mockRepo.Setup(repo => repo.GetAuthorsById(ids, It.IsAny<bool>()))
                .ReturnsAsync(authors);

            var result = await mockRepo.Object.GetAuthorsById(ids, false);


            Assert.Equal(3, result.Count());
            Assert.All(result, author => Assert.Contains(author.AuthorId, ids));



        }


    }
}
