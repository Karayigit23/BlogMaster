using BlogMaster.Core.Entity;
using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.ArticleVote;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using BlogMaster.Core.Query.Article;

namespace BlogMaster.Test.Query.ArticleVote
{
    public class GetUserVotesQueryHandlerTest
    {
        private Mock<IArticleVoteRepository> _articleVoteRepositoryMock;
        private Mock<ILogger<GetUserVotesQueryHandler>> _loggerMock;
        private GetUserVotesQueryHandler _getUserVotesQueryHandler;

        public GetUserVotesQueryHandlerTest()
        {
            _articleVoteRepositoryMock = new Mock<IArticleVoteRepository>();
            _loggerMock = new Mock<ILogger<GetUserVotesQueryHandler>>();
            _getUserVotesQueryHandler =
                new GetUserVotesQueryHandler(_articleVoteRepositoryMock.Object, _loggerMock.Object);
            
        }

        [SetUp]
        public void Setup()
        {
          _articleVoteRepositoryMock.Invocations.Clear();
        }

        [Test]
        public async Task GetUserVotesQueryHandler_ShouldReturnListOfArticleVotes_WhenUserExists()
        {
            // Arrange
            var userId = 1;
            var articleVotes = new List<Core.Entity.ArticleVote>
                { new Core.Entity.ArticleVote() { Id = 1 }, new Core.Entity.ArticleVote() { Id = 2 } };
            _articleVoteRepositoryMock.Setup(x => x.GetUserVotes(userId)).ReturnsAsync(articleVotes);

            // Act
            var query = new GetArticleVotesByUserQuery() { UserId = userId };
            var result = await _getUserVotesQueryHandler.Handle(query, new CancellationToken());

            // Assert
            result.Should().NotBeNull();
            result.Should().HaveCount(2);
            _articleVoteRepositoryMock.Verify(x => x.GetUserVotes(userId), Times.Once);
        }

        [Test]
        public void GetUserVotesQueryHandler_ShouldThrowUserNotFoundException_WhenUserNotFound()
        {
            // Arrange
            var userId = 1;
            List<Core.Entity.ArticleVote> articleVotes = null;
            _articleVoteRepositoryMock.Setup(x => x.GetUserVotes(userId)).ReturnsAsync(articleVotes);

            // Act
            var query = new GetArticleVotesByUserQuery() { UserId = userId };
            Func<Task> action = async () => await _getUserVotesQueryHandler.Handle(query, new CancellationToken());

            // Assert
            action.Should().ThrowAsync<Exception>();
            _articleVoteRepositoryMock.Verify(x => x.GetUserVotes(userId), Times.Once);
        }

        [Test]
        public void GetUserVotesQueryHandler_ShouldThrowException_WhenUserNotFound()
        {
            // Arrange
            var userId = 1;
            List<Core.Entity.ArticleVote> articleVotes = null;
            _articleVoteRepositoryMock.Setup(x => x.GetUserVotes(userId)).ReturnsAsync(articleVotes);

            // Act
            var query = new GetArticleVotesByUserQuery() { UserId = userId };
            Func<Task> action = async () => await _getUserVotesQueryHandler.Handle(query, new CancellationToken());

            // Assert
            action.Should().ThrowAsync<Exception>().WithMessage($"user not found userId: {userId}");
            _articleVoteRepositoryMock.Verify(x => x.GetUserVotes(userId), Times.Once);
        }
    }
}
   
