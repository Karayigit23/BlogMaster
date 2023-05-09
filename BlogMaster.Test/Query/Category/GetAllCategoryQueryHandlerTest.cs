using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Category;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.Category;

public class GetAllCategoryQueryHandlerTest
{
    private Mock<ICategoryRepository> _categoryRepositoryMock;
    private Mock<ILogger<GetAllCategoryQuery.GetAllCategoryQueryHandler>> _loggerMock;
    private GetAllCategoryQuery.GetAllCategoryQueryHandler _handler;

    public GetAllCategoryQueryHandlerTest()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _loggerMock = new Mock<ILogger<GetAllCategoryQuery.GetAllCategoryQueryHandler>>();
        _handler = new GetAllCategoryQuery.GetAllCategoryQueryHandler(_categoryRepositoryMock.Object,_loggerMock.Object);
    }

    [SetUp]
        public void Setup()
        {
            _categoryRepositoryMock.Invocations.Clear();
        }

        [Test]
        public async Task Handle_ReturnsExpectedResult()
        {
            // Arrange
            var expectedCategories = new List<Core.Entity.Category>
            {
                new Core.Entity.Category { Id = 1, Name = "Category 1", Description = "Description 1" },
                new Core.Entity.Category { Id = 2, Name = "Category 2", Description = "Description 2" },
                new Core.Entity.Category { Id = 3, Name = "Category 3", Description = "Description 3" }
            };
            _categoryRepositoryMock.Setup(m => m.GetAllCategori()).ReturnsAsync(expectedCategories);

            var query = new GetAllCategoryQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(expectedCategories.Count, result.Count);
            for (int i = 0; i < expectedCategories.Count; i++)
            {
                Assert.AreEqual(expectedCategories[i].Id, result[i].Id);
                Assert.AreEqual(expectedCategories[i].Name, result[i].Name);
                Assert.AreEqual(expectedCategories[i].Description, result[i].Description);
            }

            // Verify
          
            _categoryRepositoryMock.Verify(x => x.GetAllCategori(), Times.Once);
        }
    }


