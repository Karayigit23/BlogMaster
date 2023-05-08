using BlogMaster.Core.Command.User;
using BlogMaster.Core.InterFaces;
using Microsoft.Extensions.Logging;
using Moq;
using NUnit.Framework;

namespace BlogMaster.Test.Command.User;

public class CreatUserCommandHandlerTest

{
    private readonly Mock<IUserRepository> _userRepository;
    private readonly Mock<ILogger<DeleteUserCommandHandler>> _logger;
    private readonly CreateUserHandler _handler;
    
    public CreatUserCommandHandlerTest()
    {
        _userRepository = new Mock<IUserRepository>();
        _logger = new Mock<ILogger<DeleteUserCommandHandler>>();
        _handler = new CreateUserHandler(_userRepository.Object);

    }  
    
    [SetUp]
    public void setUp()
    {
      _userRepository.Invocations.Clear();
    }

    [Test]
    public async Task handle_when_user_exist_then_throw_exception()
    {

        var user = new CreateUserCommand
        {
            UserName = "Testuser",
            FirstName = "Test",
            LastName = "User",
            Email = "Testuser@gamil.com",
            Password = "TestUsErpassword"

        };
      
        await _handler.Handle(user, default);
       
        _userRepository.Verify(p=>p.AddUser(It.IsAny<Core.Entity.User>()),Times.Once);
        
        
    }
    [Test]
    public void handle_when_null_command_then_throws_nullexception()
    {
       
        CreateUserCommand command = null;

       
        Assert.ThrowsAsync<ArgumentNullException>(async () => await _handler.Handle(command, default));
        _userRepository.Verify(p => p.AddUser(It.IsAny<Core.Entity.User>()), Times.Never);
    }

    [Test]
    public void handle_when_duplicate_user_throws_exception()
    {
        // Arrange
        var command = new CreateUserCommand
        {
            UserName = "Testuser",
            FirstName = "Test",
            LastName = "User",
            Email = "Testuser@gmail.com",
            Password = "TestUserpassword"
        };
        _userRepository.Setup(p => p.GetUserByUsername(command.UserName)).ReturnsAsync(new Core.Entity.User());

        
        Assert.ThrowsAsync<InvalidOperationException>(async () => await _handler.Handle(command, default));
        _userRepository.Verify(p => p.GetUserByUsername(command.UserName), Times.Once);

    }

}
