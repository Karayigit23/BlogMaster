using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Comment;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.Comment;

public class GetAllCommentQueryHandlerTest
{
    private Mock<ICommentRepository> _commentRepository;
    private Mock<ILogger<GetAllCommentQuery.GetAllCommentQueryHandler>> _logger; 
    private IRequestHandler<GetAllCommentQuery, List<Core.Entity.Comment>> _handler;

    public GetAllCommentQueryHandlerTest()
    {
        _commentRepository = new Mock<ICommentRepository>();
        _logger = new Mock<ILogger<GetAllCommentQuery.GetAllCommentQueryHandler>>();
        _handler = new GetAllCommentQuery.GetAllCommentQueryHandler(_commentRepository.Object, _logger.Object);
    }
    

    [SetUp]
        public void Setup()
        {
            _commentRepository.Invocations.Clear();
            
        }

        [Test]
        public async Task Handle_ReturnsAllComments()
        {
            // Arrange
            var comments = new List<Core.Entity.Comment>
            {
                new Core.Entity.Comment { Id = 1, Content = "Comment 1", Author = "Author 1", PublishDate = DateTime.Now },
                new Core.Entity.Comment { Id = 2, Content = "Comment 2", Author = "Author 2", PublishDate = DateTime.Now },
                new Core.Entity.Comment { Id = 3, Content = "Comment 3", Author = "Author 3", PublishDate = DateTime.Now }
            };
            _commentRepository.Setup(x => x.GetAllComments()).ReturnsAsync(comments);

            var query = new GetAllCommentQuery();

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.AreEqual(comments.Count, result.Count);
            Assert.AreEqual(comments[0].Content, result[0].Content);
            Assert.AreEqual(comments[1].Author, result[1].Author);
            Assert.AreEqual(comments[2].PublishDate, result[2].PublishDate);

            _logger.Verify(x => x.Log(LogLevel.Information, It.IsAny<EventId>(), It.IsAny<It.IsAnyType>(), null, It.IsAny<Func<It.IsAnyType, Exception, string>>()), Times.Once);
        }
    }

