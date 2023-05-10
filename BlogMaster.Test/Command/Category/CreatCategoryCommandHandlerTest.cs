using BlogMaster.Core.Command.Category;
using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using Moq;

namespace BlogMaster.Test.Command.Category;

public class CreatCategoryCommandHandlerTest
{
    private Mock<ICategoryRepository> _categoryRepositoryMock;
    private CreateCategoryCommandHandler _handler;

    public CreatCategoryCommandHandlerTest()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _handler = new CreateCategoryCommandHandler(_categoryRepositoryMock.Object);   
    }

    [SetUp]
        public void Setup()
        {
          _categoryRepositoryMock.Invocations.Clear();
        }
        [Test]
        public async Task Handle_ValidCategory_ReturnsCreatedCategory()
        {
            // Arrange
            var category = new Core.Entity.Category { Name = "Test Category", Description = "This is a test category", Articles = new List<Core.Entity.Article>() };
            _categoryRepositoryMock.Setup(m => m.AddCategory(It.IsAny<Core.Entity.Category>())).ReturnsAsync(category);

            var command = new CreateCategoryCommand { Name = "Test Category", Description = "This is a test category" };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.IsInstanceOf<Core.Entity.Category>(result);
            Assert.AreEqual(category.Name, result.Name);
            Assert.AreEqual(category.Description, result.Description);
            Assert.AreEqual(category.Articles.Count, result.Articles.Count);

            // Verify
            _categoryRepositoryMock.Verify(m => m.AddCategory(It.IsAny<Core.Entity.Category>()), Times.Once);
        }


    }


