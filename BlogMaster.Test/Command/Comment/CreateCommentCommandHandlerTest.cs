using BlogMaster.Core.Command.Comment;
using BlogMaster.Core.InterFaces;
using Moq;

namespace BlogMaster.Test.Command.Comment;

public class CreateCommentCommandHandlerTest
{
    private Mock<ICommentRepository> _commentRepository;
    private CreateCommentCommandHandler _handler;

    public CreateCommentCommandHandlerTest()
    {
        _commentRepository = new Mock<ICommentRepository>();
        _handler = new CreateCommentCommandHandler(_commentRepository.Object);
        
    }

    [SetUp]
        public void Setup()
        {
          _commentRepository.Invocations.Clear();
        }

        [Test]
        public async Task Handle_ShouldAddCommentToRepository()
        {
            var command = new CreateCommentCommand
            {
                Content = "Test comment",
                PublishDate = DateTime.Now,
                Author = "Test author",
                UserId = 1,
                ArticleId = 1
            };

          
            var result = await _handler.Handle(command, CancellationToken.None);
 
            _commentRepository.Verify(x => x.AddComment(It.IsAny<Core.Entity.Comment>()), Times.Once);
        }

     
    }

