using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Article;
using BlogMaster.Core.Query.BlackList;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.BlackList;

public class GetBlackListByIdQueryHandlerTests
{
    private Mock<IBlacklistRepository> _mockRepository;
        private Mock<ILogger<GetBlackListByIdQueryHandler>> _mockLogger;
        private IRequestHandler<GetBlackListByIdQuery, Core.Entity.BlackList> _handler;

        public GetBlackListByIdQueryHandlerTests()
        {
            _mockRepository = new Mock<IBlacklistRepository>();
            _mockLogger = new Mock<ILogger<GetBlackListByIdQueryHandler>>();
            _handler = new GetBlackListByIdQueryHandler(_mockRepository.Object, _mockLogger.Object);
        }

        [SetUp]
        public void Setup()
        {
           _mockRepository.Invocations.Clear();
        }

        [Test]
        public async Task Handle_WithValidId_ReturnsBlacklistItem()
        {
            // Arrange
            int id = 1;
            int articleıd = 2;
            DateTime dateTime=DateTime.Now;    
                
            var expectedResult = new Core.Entity.BlackList { Id = id,ArticleId =articleıd,BlacklistDate =dateTime };
            _mockRepository.Setup(x => x.GetBlacklistById(id)).ReturnsAsync(expectedResult);

            var query = new GetBlackListByIdQuery { Id = id };

            // Act
            var result = await _handler.Handle(query, default);

            // Assert
            Assert.AreEqual(expectedResult, result);
            
        }

        [Test]
        public void Handle_WithInvalidId_ThrowsException()
        {
            // Arrange
            int id = 1;
            _mockRepository.Setup(x => x.GetBlacklistById(id)).ReturnsAsync(null as Core.Entity.BlackList);

            var query = new GetBlackListByIdQuery { Id = id };

            // Act & Assert
            Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(query, default));
          
        }
    }

