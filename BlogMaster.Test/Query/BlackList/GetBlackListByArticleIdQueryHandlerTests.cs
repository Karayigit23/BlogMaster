using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.BlackList;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.BlackList;

public class GetBlackListByArticleIdQueryHandlerTests
{
    private Mock<IBlacklistRepository> _blacklistRepositoryMock;
    private Mock<ILogger<GetBlackListByArticleIdQueryHandler>> _loggerMock;
    private GetBlackListByArticleIdQueryHandler _handler;

    [SetUp]
    public void Setup()
    {
        _blacklistRepositoryMock = new Mock<IBlacklistRepository>();
        _loggerMock = new Mock<ILogger<GetBlackListByArticleIdQueryHandler>>();

        _handler = new GetBlackListByArticleIdQueryHandler(_blacklistRepositoryMock.Object, _loggerMock.Object);
    }

    [Test]
    public async Task Handle_ReturnsList_WhenBlackListExistsForArticle()
    {
        // Arrange
        int articleId = 123;
        var blackList = new List<Core.Entity.BlackList>
        {
            new Core.Entity.BlackList { Id = 1, ArticleId = articleId, UserId = 456 },
            new Core.Entity.BlackList { Id = 2, ArticleId = articleId, UserId = 789 }
        };

        _blacklistRepositoryMock.Setup(x => x.GetBlacklistedByArticleId(articleId))
            .ReturnsAsync(blackList);

        var query = new GetBlackListByArticleIdQuery { ArticleId = articleId };

        // Act
        var result = await _handler.Handle(query, default);

        // Assert
        Assert.AreEqual(blackList, result);
        
    }

    [Test]
    public async Task Handle_ThrowsException_WhenBlackListDoesNotExistForArticle()
    {
        // Arrange
        int articleId = 123;
        List<Core.Entity.BlackList> blackList = null;

        _blacklistRepositoryMock.Setup(x => x.GetBlacklistedByArticleId(articleId))
            .ReturnsAsync(blackList);

        var query = new GetBlackListByArticleIdQuery { ArticleId = articleId };

        // Act & Assert
        var ex = Assert.ThrowsAsync<NotFoundException>(() => _handler.Handle(query, default));
        Assert.AreEqual($"blacklist not found articleId:{articleId}", ex.Message);

        _loggerMock.Verify(x => x.Log(
            LogLevel.Information,
            It.IsAny<EventId>(),
            It.Is<It.IsAnyType>((v, t) => v.ToString().Contains(articleId.ToString())),
            It.IsAny<Exception>(),
            It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
    }
}

