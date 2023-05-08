using BlogMaster.Core.Command.User;
using BlogMaster.Core.InterFaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Command.User;


public class DeleteUserCommandHandlerTest
{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<ILogger<DeleteUserCommandHandler>> _logger;
    private readonly DeleteUserCommandHandler _handler;

    public DeleteUserCommandHandlerTest()
    {
        _userRepository = new Mock<IUserRepository>();
        _logger = new Mock<ILogger<DeleteUserCommandHandler>>();
        _handler = new DeleteUserCommandHandler(_userRepository.Object);
    }

    [SetUp]
    public void SetUp()
    {
        _userRepository.Invocations.Clear();
    }

    [Test]
    public async Task Handle_WhenUserExists_DeletesUser()
    {
        
        var user = new Core.Entity.User
        {
            Id = 1,
            UserName = "Testuser",
            FirstName = "Test",
            LastName = "User",
            Email = "testuser@gmail.com",
            Password = "testpassword"
        };

        _userRepository.Setup(x => x.GetUserById(user.Id)).ReturnsAsync(user);

        await _handler.Handle(new DeleteUserCommand { Id = user.Id }, CancellationToken.None);

        
        _userRepository.Verify(x => x.DeleteUser(user), Times.Once);
    }
    [SetUp]
    public void SetUp2()
    {
        _userRepository.Invocations.Clear();
    }

    [Test]
    public void Handle_WhenUserDoesNotExist_ThrowsException()
    {
        var userId = 1;

        _userRepository.Setup(x => x.GetUserById(userId)).ReturnsAsync(null as Core.Entity.User);

        
        var ex = Assert.ThrowsAsync<Exception>(() => _handler.Handle(new DeleteUserCommand { Id = userId }, CancellationToken.None));
        Assert.AreEqual($"User with ID {userId} not found.", ex.Message);
    }
}

