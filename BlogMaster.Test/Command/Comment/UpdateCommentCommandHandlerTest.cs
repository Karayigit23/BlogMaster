using BlogMaster.Core.Command.Comment;
using BlogMaster.Core.Exception;
using BlogMaster.Core.InterFaces;
using Moq;

namespace BlogMaster.Test.Command.Comment;

public class UpdateCommentCommandHandlerTest
{
    private Mock<ICommentRepository> _commentRepository;
    private UpdateCommentCommandHandler _Handler;

    public UpdateCommentCommandHandlerTest()
    {
        _commentRepository= new Mock<ICommentRepository>();
        _Handler = new UpdateCommentCommandHandler(_commentRepository.Object);
    }

    [SetUp]
        public void Setup()
        {
            _commentRepository.Invocations.Clear();
        }

       
        [Test]
        public async Task Handle_ValidComment_ReturnsUpdatedComment()
        {
        
            var comment = new Core.Entity.Comment
            {
                Id = 1,
                Content = "Old content",
                Author = "Old author",
                PublishDate = DateTime.Today.AddDays(-5)
                
            };

            var updateCommentCommand = new UpdateCommentCommand
            {
                Id = comment.Id,
                Content = "New content",
                Author = "New author"
            };

            _commentRepository.Setup(x => x.GetCommentById(comment.Id)).ReturnsAsync(comment);

            // Act
            var result = await _Handler.Handle(updateCommentCommand, CancellationToken.None);

            // Assert
            Assert.AreEqual(updateCommentCommand.Content, result.Content);
            Assert.AreEqual(updateCommentCommand.Author, result.Author);
            var expectedPublishDate = DateTime.Now;
            Assert.AreEqual(expectedPublishDate.Date, result.PublishDate.Date);

            comment.Content = updateCommentCommand.Content;
            comment.Author = updateCommentCommand.Author;
            comment.PublishDate = DateTime.Now;
           

            Assert.AreEqual(comment.Content, result.Content);
            Assert.AreEqual(comment.Author, result.Author);
            

            _commentRepository.Verify(x => x.UpdateComment(comment), Times.Once);
        }


        [Test]
        public void Handle_InvalidComment_ThrowsException()
        {
          
            var commentId = 1;
            var updateCommentCommand = new UpdateCommentCommand
            {
                Id = commentId,
                Content = "content",
                Author = "author"
            };

            _commentRepository.Setup(x => x.GetCommentById(commentId)).ReturnsAsync((Core.Entity.Comment)null);

            // Act and assert
            Assert.ThrowsAsync<NotFoundException>(() => _Handler.Handle(updateCommentCommand, CancellationToken.None));
        }
    }
