
using BlogMaster.Core.Command.Comment;
using BlogMaster.Core.InterFaces;
using Moq;

namespace BlogMaster.Test.Command.Comment
{
    public class DeleteCommentCommandHandlerTest
    {
        private Mock<ICommentRepository> _commentRepository;
        private DeleteCommentCommandHandler _handler;

        public DeleteCommentCommandHandlerTest()
        {
            _commentRepository = new Mock<ICommentRepository>();
            _handler = new DeleteCommentCommandHandler(_commentRepository.Object);
        }
        [SetUp]
        public void Setup()
        {
            _commentRepository.Invocations.Clear();
        }

        
        [Test]
        public async Task Handle_ShouldDeleteComment()
        {
            // Arrange
            var commentId = 1;
            var command = new DeleteCommentCommand { Id = commentId };
            var comment = new Core.Entity.Comment { Id = commentId };
            _commentRepository.Setup(x => x.GetCommentById(commentId)).ReturnsAsync(comment);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            _commentRepository.Verify(x => x.DeleteComment(comment), Times.Once);
        }

        [Test]
        public void Handle_ShouldThrowException_WhenCommentNotFound()
        {
            
            var commentId = 1;
            var command = new DeleteCommentCommand { Id = commentId };
            _commentRepository.Setup(x => x.GetCommentById(commentId)).ReturnsAsync((Core.Entity.Comment)null);

            
            Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}

