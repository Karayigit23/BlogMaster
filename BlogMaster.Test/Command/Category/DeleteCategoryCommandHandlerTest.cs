using BlogMaster.Core.Command.Category;
using BlogMaster.Core.InterFaces;
using Moq;

namespace BlogMaster.Test.Command.Category;

public class DeleteCategoryCommandHandlerTest
{
    private Mock<ICategoryRepository> _categoryRepositoryMock;
    private DeleteCategoryCommandHandler _handler;

    public DeleteCategoryCommandHandlerTest()
    {
       _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _handler = new DeleteCategoryCommandHandler(_categoryRepositoryMock.Object);
    }


    [SetUp]
    public void Setup()
    {
      _categoryRepositoryMock.Invocations.Clear();
    }

[Test]
public async Task Handle_ValidCategoryId_DeletesCategory()
{
    // Arrange
    var categoryId = 1;
    var category = new Core.Entity.Category { Id = categoryId, Name = "Test Category", Description = "This is a test category" };
    _categoryRepositoryMock.Setup(m => m.GetCategoryById(categoryId)).ReturnsAsync(category);

    var command = new DeleteCategoryCommand { Id = categoryId };

    // Act
    await _handler.Handle(command, CancellationToken.None);

    // Assert
    _categoryRepositoryMock.Verify(m => m.DeleteCategory(category), Times.Once);
}

[Test]
public void Handle_InvalidCategoryId_ThrowsException()
{
    // Arrange
    var categoryId = 1;
    _categoryRepositoryMock.Setup(m => m.GetCategoryById(categoryId)).ReturnsAsync((Core.Entity.Category)null);

    var command = new DeleteCategoryCommand { Id = categoryId };

    // Act + Assert
    Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
}
}

