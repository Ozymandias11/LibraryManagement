//using Library.Data.Library.Interfaces;
//using Library.Model.Models;
//using Moq;
//using System;
//using System.Collections.Generic;
//using System.Linq;
//using System.Text;
//using System.Threading.Tasks;
//using UnitTests.TestData;

//namespace UnitTests.Repository
//{
//   public class CategoryRepositoryTests
//    {
//        [Fact]
//        //public async Task GetAllCategories_ReturnsAllNonDeletedCategories()
//        //{
//        //    var mockRepo = new Mock<ICategoryRepository>();
//        //    var categories = CategoryFaker.Create().Generate(3);
//        //    mockRepo.Setup(repo => repo.GetAllCategories(false))
//        //        .ReturnsAsync(categories.AsEnumerable());

//        //    var result = await mockRepo.Object.GetAllCategories(false);

//        //    Assert.IsType<List<Category>>(result);
//        //    Assert.Equal(3, result.Count());
//        //    Assert.All(result, category => Assert.Null(category.DeletedDate));
//        //}

//        //[Fact]
//        //public async Task GetCategory_ReturnsNull_WhenCategoryNotFound()
//        //{
//        //    var mockRepo = new Mock<ICategoryRepository>();
//        //    var nonExistentId = Guid.NewGuid();
//        //    mockRepo.Setup(repo => repo.GetCategory(nonExistentId, It.IsAny<bool>()))
//        //        .ReturnsAsync((Category?)null);

//        //    var result = await mockRepo.Object.GetCategory(nonExistentId, false);

//        //    Assert.Null(result);
//        //}

//        //[Fact]
//        //public async Task GetCategory_ReturnsCorrectCategory()
//        //{
//        //    var mockRepo = new Mock<ICategoryRepository>();
//        //    var category = CategoryFaker.Create().Generate();
//        //    mockRepo.Setup(repo => repo.GetCategory(category.CategoryId, It.IsAny<bool>()))
//        //        .ReturnsAsync(category);

//        //    var result = await mockRepo.Object.GetCategory(category.CategoryId, false);

//        //    Assert.NotNull(result);
//        //    Assert.Equal(category.CategoryId, result.CategoryId);
//        //}

//        //[Fact]
//        //public async Task GetCategoryOfBooks_ReturnsEmptyList_WhenNoCategoriesForBookFound()
//        //{
//        //    var mockRepo = new Mock<ICategoryRepository>();
//        //    var bookId = Guid.NewGuid();
//        //    mockRepo.Setup(repo => repo.GetCategoryOfBooks(bookId, It.IsAny<bool>()))
//        //        .ReturnsAsync(new List<Category>());

//        //    var result = await mockRepo.Object.GetCategoryOfBooks(bookId, false);

//        //    Assert.Empty(result);
//        //}

//        [Fact]
//        public async Task GetCategoriesById_ReturnsCorrectCategories()
//        {
//            var mockRepo = new Mock<ICategoryRepository>();
//            var categories = CategoryFaker.Create().Generate(3);
//            var ids = categories.Select(c => c.CategoryId).ToList();
//            mockRepo.Setup(repo => repo.GetCategoriesById(ids, It.IsAny<bool>()))
//                .ReturnsAsync(categories);

//            var result = await mockRepo.Object.GetCategoriesById(ids, false);

//            Assert.Equal(3, result.Count());
//            Assert.All(result, category => Assert.Contains(category.CategoryId, ids));
//        }

//        [Fact]
//        public async Task GetCategoryByTitle_ReturnsCorrectCategory()
//        {
//            var mockRepo = new Mock<ICategoryRepository>();
//            var category = CategoryFaker.Create().Generate();
//            mockRepo.Setup(repo => repo.GetCatgeoryByTitle(category.Title, It.IsAny<bool>()))
//                .ReturnsAsync(category);

//            var result = await mockRepo.Object.GetCatgeoryByTitle(category.Title, false);

//            Assert.NotNull(result);
//            Assert.Equal(category.Title, result.Title);
//        }

//    }
//}
