using Library.Data.Library.Interfaces;
using Library.Model.Models;
using Moq;
using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using UnitTests.TestData;

namespace UnitTests.Repository
{
    public class PublisherReposiotryTests
    {
       
        //public async Task GetAllPublisher_ReturnsAllNonDeletedPublishers()
        //{
        //    var mockRepo = new Mock<IPublisherRepository>();
        //    var publishers = PublisherFaker.Create().Generate(3);
        //    mockRepo.Setup(repo => repo.GetAllPublisher(false))
        //        .ReturnsAsync(publishers.OrderBy(p => p.CreatedDate));

        //    var result = await mockRepo.Object.GetAllPublisher(false);

        //    Assert.Equal(3, result.Count());
        //    Assert.All(result, publisher => Assert.Null(publisher.DeletedDate));
        //    Assert.Equal(publishers.OrderBy(p => p.CreatedDate), result);
        //}

        //[Fact]
        //public async Task GetPublisher_ReturnsNull_WhenPublisherNotFound()
        //{
        //    var mockRepo = new Mock<IPublisherRepository>();
        //    var nonExistentId = Guid.NewGuid();
        //    mockRepo.Setup(repo => repo.GetPublisher(nonExistentId, It.IsAny<bool>()))
        //        .ReturnsAsync((Publisher?)null);

        //    var result = await mockRepo.Object.GetPublisher(nonExistentId, false);

        //    Assert.Null(result);
        //}

        [Fact]
        public async Task GetPublisher_ReturnsCorrectPublisher()
        {
            var mockRepo = new Mock<IPublisherRepository>();
            var publisher = PublisherFaker.Create().Generate();
            mockRepo.Setup(repo => repo.GetPublisher(publisher.PublisherId, It.IsAny<bool>()))
                .ReturnsAsync(publisher);

            var result = await mockRepo.Object.GetPublisher(publisher.PublisherId, false);

            Assert.NotNull(result);
            Assert.Equal(publisher.PublisherId, result.PublisherId);
        }

        [Fact]
        public async Task GetPublishersOfBook_ReturnsCorrectPublishers()
        {
            var mockRepo = new Mock<IPublisherRepository>();
            var bookId = Guid.NewGuid();
            var publishers = PublisherFaker.Create().Generate(2);
            mockRepo.Setup(repo => repo.GetPublishersOfBook(bookId, It.IsAny<bool>()))
                .ReturnsAsync(publishers);

            var result = await mockRepo.Object.GetPublishersOfBook(bookId, false);

            Assert.Equal(2, result.Count());
            Assert.Equal(publishers, result);
        }

        [Fact]
        public async Task GetPublishersById_ReturnsCorrectPublishers()
        {
            var mockRepo = new Mock<IPublisherRepository>();
            var publishers = PublisherFaker.Create().Generate(3);
            var ids = publishers.Select(p => p.PublisherId).ToList();
            mockRepo.Setup(repo => repo.GetPublishersById(ids, It.IsAny<bool>()))
                .ReturnsAsync(publishers);

            var result = await mockRepo.Object.GetPublishersById(ids, false);

            Assert.Equal(3, result.Count());
            Assert.All(result, publisher => Assert.Contains(publisher.PublisherId, ids));
        }

        [Fact]
        public async Task GetPublisherWithTitle_ReturnsCorrectPublisher()
        {
            var mockRepo = new Mock<IPublisherRepository>();
            var publisher = PublisherFaker.Create().Generate();
            mockRepo.Setup(repo => repo.GetPublisherWithTitle(publisher.PublisherName, It.IsAny<bool>()))
                .ReturnsAsync(publisher);

            var result = await mockRepo.Object.GetPublisherWithTitle(publisher.PublisherName, false);

            Assert.NotNull(result);
            Assert.Equal(publisher.PublisherName, result.PublisherName);
        }
    }
}
