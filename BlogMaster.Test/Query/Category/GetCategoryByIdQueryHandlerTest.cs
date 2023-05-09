using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Category;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.Category;

public class GetCategoryByIdQueryHandlerTest
{
    private Mock<ICategoryRepository> _mockCategoryRepository;
    private Mock<ILogger<GetCategoryByIdQueryHandler>> _mockLogger;
    private IRequestHandler<GetCategoryByIdQuery, Core.Entity.Category> _handler;

    public GetCategoryByIdQueryHandlerTest()
    {
        _mockCategoryRepository = new Mock<ICategoryRepository>();
        _mockLogger = new Mock<ILogger<GetCategoryByIdQueryHandler>>();
        _handler = new GetCategoryByIdQueryHandler(_mockCategoryRepository.Object, _mockLogger.Object);
    }

    [SetUp]
    public void Setup()
    {
        _mockCategoryRepository.Invocations.Clear();
    }

        [Test]
        public async Task Handle_WithValidId_ReturnsCategory()
        {
            // Arrange
            var category = new Core.Entity.Category { Id = 1, Name = "Test Category" };
            _mockCategoryRepository.Setup(repo => repo.GetCategoryById(It.IsAny<int>()))
                                   .ReturnsAsync(category);

            // Act
            var result = await _handler.Handle(new GetCategoryByIdQuery { Id = 1 }, CancellationToken.None);

            // Assert
            Assert.IsNotNull(result);
            Assert.AreEqual(category.Id, result.Id);
            Assert.AreEqual(category.Name, result.Name);
            _mockCategoryRepository.Verify(repo => repo.GetCategoryById(1), Times.Once);
          
        }

        [Test]
        public async Task Handle_WithInvalidId_ReturnsNull()
        {
            // Arrange
            _mockCategoryRepository.Setup(repo => repo.GetCategoryById(It.IsAny<int>()))
                                   .ReturnsAsync((Core.Entity.Category)null);

            // Act
            var result = await _handler.Handle(new GetCategoryByIdQuery { Id = 1 }, CancellationToken.None);

            // Assert
            Assert.IsNull(result);
            _mockCategoryRepository.Verify(repo => repo.GetCategoryById(1), Times.Once);
            
        }
    }


