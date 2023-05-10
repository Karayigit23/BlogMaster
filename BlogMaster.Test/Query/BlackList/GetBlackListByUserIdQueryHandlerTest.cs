using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Article;
using BlogMaster.Core.Query.BlackList;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.BlackList

{
    public class GetBlackListByUserIdQueryHandlerTest
    {
        private Mock<IBlacklistRepository> _mockRepository;
            private Mock<ILogger<GetBlackListByUserIdQueryHandler>> _mockLogger;
            private IRequestHandler<GetBlackListByUserIdQuery, List<Core.Entity.BlackList>> _handler;

            public GetBlackListByUserIdQueryHandlerTest()
            {
                _mockRepository = new Mock<IBlacklistRepository>();
                _mockLogger = new Mock<ILogger<GetBlackListByUserIdQueryHandler>>();
                _handler = new GetBlackListByUserIdQueryHandler(_mockRepository.Object, _mockLogger.Object);
            }
            [SetUp]
            public void Setup()
            {
                _mockRepository.Invocations.Clear();
            }

           

            [Test]
            public void Handle_WithInvalidUserId_ThrowsException()
            {
                // Arrange
                int userId = 1;
                _mockRepository.Setup(x => x.GetBlacklistByUserId(userId)).ReturnsAsync(null as List<Core.Entity.BlackList>);

                var query = new GetBlackListByUserIdQuery { UserId = userId };

                // Act & Assert
                Assert.ThrowsAsync<NotFoundException>(async () => await _handler.Handle(query, default));
               
            }


   

    }
}   
