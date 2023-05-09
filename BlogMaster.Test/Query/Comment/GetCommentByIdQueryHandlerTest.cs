using BlogMaster.Core.InterFaces;
using BlogMaster.Core.Query.Comment;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Query.Comment;

public class GetCommentByIdQueryHandlerTest
{
    private readonly Mock<ICommentRepository> _commentRepository;
    private readonly Mock<ILogger<GetCommentByIdQueryHandler>> _logger;

        public GetCommentByIdQueryHandlerTest()
        {
            _commentRepository = new Mock<ICommentRepository>();
            _logger = new Mock<ILogger<GetCommentByIdQueryHandler>>();
        }

        [SetUp]
        public void SetUp()
        {
            _commentRepository.Invocations.Clear();
        }

        [Test]
        public async Task Handle_ExistingCommentId_ReturnsComment()
        {
            // Arrange
            var existingCommentId = 1;
            var existingComment = new Core.Entity.Comment
            {
                Id = existingCommentId,
                Content = "Some content",
                Author = "Some author",
                PublishDate = DateTime.Now
            };

            _commentRepository
                .Setup(x => x.GetCommentById(existingCommentId))
                .ReturnsAsync(existingComment);

            var query = new GetCommentByIdQuery { Id = existingCommentId };
            var handler = new GetCommentByIdQueryHandler(_commentRepository.Object, _logger.Object);

            // Act
            var result = await handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.NotNull(result);
            Assert.AreEqual(existingComment.Id, result.Id);
            Assert.AreEqual(existingComment.Content, result.Content);
            Assert.AreEqual(existingComment.Author, result.Author);
            Assert.AreEqual(existingComment.PublishDate, result.PublishDate);

            _commentRepository.Verify(x => x.GetCommentById(existingCommentId), Times.Once);
        }

        [Test]
        public async Task Handle_NonExistingCommentId_ThrowsException()
        {
            // Arrange
            var nonExistingCommentId = 2;
            Core.Entity.Comment nullComment = null;

            _commentRepository
                .Setup(x => x.GetCommentById(nonExistingCommentId))
                .ReturnsAsync(nullComment);

            var query = new GetCommentByIdQuery { Id = nonExistingCommentId };
            var handler = new GetCommentByIdQueryHandler(_commentRepository.Object, _logger.Object);

            Assert.ThrowsAsync<Exception>(() => handler.Handle(query, CancellationToken.None));

            _commentRepository.Verify(x => x.GetCommentById(nonExistingCommentId), Times.Once);
        }
    }

