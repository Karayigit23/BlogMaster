using BlogMaster.Core.Command.BlackList;
using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using Moq;
using NUnit.Framework;
using System;
using System.Threading;
using BlogMaster.Core.InterFaces;

namespace BlogMaster.Test.Command.BlackList
{
    public class CreateBlackListCommandHandlerTest
    { 
        private readonly Mock<IBlacklistRepository> _blacklistRepositoryMock;
        private readonly CreateBlackListCommandHandler _handler;

        public CreateBlackListCommandHandlerTest()
        {
            _blacklistRepositoryMock = new Mock<IBlacklistRepository>();
            _handler = new CreateBlackListCommandHandler(_blacklistRepositoryMock.Object);
        }

        [SetUp]
        public void SetUp()
        {
            _blacklistRepositoryMock.Invocations.Clear();
        }
        [Test]
        public async void Handle_UserNotBlacklisted_ReturnsCreatedBlackList()
        {
            // Arrange
            var articleId = 1;
            var userId = 1;
            _blacklistRepositoryMock.Setup(x => x.IsArticleBlacklistedForUser(articleId, userId)).ReturnsAsync(false);
            _blacklistRepositoryMock.Setup(x => x.AddToBlacklist(It.IsAny<Core.Entity.BlackList>()))
                .ReturnsAsync.(new Core.Entity.BlackList()
            {
                ArticleId = articleId,
                UserId = userId,
                BlacklistDate = DateTime.Now
            };

            var command = new CreateBlackListCommand
            {
                ArticleId = articleId,
                UserId = userId
            };

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.IsInstanceOf<Core.Entity.BlackList>(result);
            Assert.AreEqual(articleId, result.ArticleId);
            Assert.AreEqual(userId, result.UserId);

            _blacklistRepositoryMock.Verify(x => x.IsArticleBlacklistedForUser(articleId, userId), Times.Once);
            _blacklistRepositoryMock.Verify(x => x.AddToBlacklist(It.IsAny<Core.Entity.BlackList>()), Times.Once);
        }

        [Test]
        public async void Handle_UserAlreadyBlacklisted_ThrowsException()
        {
            // Arrange
            var articleId = 1;
            var userId = 1;
            _blacklistRepositoryMock.Setup(x => x.IsArticleBlacklistedForUser(articleId, userId)).ReturnsAsync(true);

            var command = new CreateBlackListCommand
            {
                ArticleId = articleId,
                UserId = userId
            };

            // Act & Assert
            var ex = Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
            Assert.AreEqual("User is already blacklisted for this article.", ex.Message);
        }
    }
}





