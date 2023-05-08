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
            UserName = "Testuser2",
            FirstName = "Test12",
            LastName = "User11",
            Email = "Testus2er@gamil.com",
            Password = "TestUsErpassword22"

        };
      
        await _handler.Handle(user, default);
       
        _userRepository.Verify(p=>p.AddUser(It.IsAny<Core.Entity.User>()),Times.Once);
        
        
    }
    [Test]
    public async Task handle_when_duplicate_user_throws_exception()
    {
        // Arrange
        var existingUser = new Core.Entity.User
        {
            UserName = "Testuser",
            FirstName = "Test",
            LastName = "User",
            Email = "Testuser@gmail.com",
            Password = "TestUsErpassword"
        };
        _userRepository.Setup(p => p.GetUserByUsername(existingUser.UserName))
            .ReturnsAsync(existingUser);

        var user = new CreateUserCommand
        {
            UserName = "Testuser",
            FirstName = "Test",
            LastName = "User",
            Email = "Testuser2@gmail.com",
            Password = "TestUsErpassword"
        };

       
        var ex = Assert.ThrowsAsync<Exception>(() => _handler.Handle(user, CancellationToken.None));
        Assert.AreEqual($"A user with username {user.UserName} already exists.", ex.Message);
    }
    

   

}
