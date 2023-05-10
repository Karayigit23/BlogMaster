using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.BlackList;
using FluentAssertions;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.BlackList;

public class GetAllBlackListQueryHandlerTest
{
    private Mock<IBlacklistRepository> _blacklistRepositoryMock;
    private Mock<ILogger<GetAllBlackListQuery.GetallBlacklistQueryHandler>> _loggerMock;
    private GetAllBlackListQuery.GetallBlacklistQueryHandler _getAllBlackListQueryHandler;
        
        [SetUp]
        public void Setup()
        {
            _blacklistRepositoryMock = new Mock<IBlacklistRepository>();
            _loggerMock = new Mock<ILogger<GetAllBlackListQuery.GetallBlacklistQueryHandler>>();
            _getAllBlackListQueryHandler = new GetAllBlackListQuery.GetallBlacklistQueryHandler(_blacklistRepositoryMock.Object, _loggerMock.Object);
        }

        [Test]
        public async Task GetAllBlackListQueryHandler_ShouldReturnAllBlackList()
        {
            // Arrange
            var blackLists = new List<Core.Entity.BlackList>()
            {
                new Core.Entity.BlackList() { Id = 1 },
                new Core.Entity.BlackList() { Id = 2 }
            };
            _blacklistRepositoryMock.Setup(x => x.GetAll()).ReturnsAsync(blackLists);

            // Act
            var query = new GetAllBlackListQuery();
            var result = await _getAllBlackListQueryHandler.Handle(query, new CancellationToken());

            // Assert
            result.Should().BeEquivalentTo(blackLists);
            _blacklistRepositoryMock.Verify(x => x.GetAll(), Times.Once);
        }
    }


