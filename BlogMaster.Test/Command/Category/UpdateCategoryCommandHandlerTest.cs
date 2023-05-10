using BlogMaster.Core.Command.Category;
using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using Moq;

namespace BlogMaster.Test.Command.Category;

public class UpdateCategoryCommandHandlerTest
{
    private Mock<ICategoryRepository> _categoryRepositoryMock;
    private UpdateCategoryCommandHandler _handler;

    public UpdateCategoryCommandHandlerTest()
    {
        _categoryRepositoryMock = new Mock<ICategoryRepository>();
        _handler = new UpdateCategoryCommandHandler(_categoryRepositoryMock.Object);
    }

    [SetUp]
    public void Setup()
    {
       _categoryRepositoryMock.Invocations.Clear();
    }

    [Test]
    public async Task Handle_ValidCategory_ReturnsUpdatedCategory()
    {
        // Arrange
        var category = new Core.Entity.Category { Id = 1, Name = "Test Category", Description = "This is a test category" };
        var updatedCategory = new Core.Entity.Category { Id = 1, Name = "Updated Category", Description = "This is an updated test category" };

        _categoryRepositoryMock.Setup(m => m.GetCategoryById(category.Id)).ReturnsAsync(category);
        _categoryRepositoryMock.Setup(m => m.UpdateCategory(category)).Returns(Task.CompletedTask);

        var command = new UpdateCategoryCommand { Id = category.Id, Name = "Updated Category", Description = "This is an updated test category" };

        // Act
        var result = await _handler.Handle(command, CancellationToken.None);

        // Assert
        Assert.IsNotNull(result);
        Assert.IsInstanceOf<Core.Entity.Category>(result);
        Assert.AreEqual(updatedCategory.Id, result.Id);
        Assert.AreEqual(updatedCategory.Name, result.Name);
        Assert.AreEqual(updatedCategory.Description, result.Description);

        // Verify
        _categoryRepositoryMock.Verify(m => m.GetCategoryById(category.Id), Times.Once);
        _categoryRepositoryMock.Verify(m => m.UpdateCategory(category), Times.Once);
    }

    [Test]
    public void Handle_InvalidCategoryId_ThrowsException()
    {
        // Arrange
        var invalidCategoryId = 0;
        _categoryRepositoryMock.Setup(m => m.GetCategoryById(invalidCategoryId)).ReturnsAsync((Core.Entity.Category)null);
        var command = new UpdateCategoryCommand { Id = invalidCategoryId, Name = "Test Category", Description = "This is a test category" };

        // Act & Assert
        Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(command, CancellationToken.None), $"not found {invalidCategoryId}");

        // Verify
        _categoryRepositoryMock.Verify(m => m.GetCategoryById(invalidCategoryId), Times.Once);
        _categoryRepositoryMock.Verify(m => m.UpdateCategory(It.IsAny<Core.Entity.Category>()), Times.Never);
    }
}

