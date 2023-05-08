using BlogMaster.Core.Command.User;
using BlogMaster.Core.InterFaces;
using Microsoft.Extensions.Logging;
using Moq;

namespace BlogMaster.Test.Command.User
{

    public class UpdateUserCommandHandlerTest
    {
        private readonly Mock<IUserRepository> _userRepository;
        private readonly Mock<ILogger<UpdateUserCommandHandler>> _logger;
        private readonly UpdateUserCommandHandler _handler;

        public UpdateUserCommandHandlerTest()
        {
            _userRepository = new Mock<IUserRepository>();
            _logger = new Mock<ILogger<UpdateUserCommandHandler>>();
            _handler = new UpdateUserCommandHandler(_userRepository.Object);
        }

        [SetUp]
        public void SetUp()
        {
           _userRepository.Invocations.Clear();
        }

        [Test]
        public async Task Handle_WhenUserExists_UpdatesUser()
        {
            // Arrange
            var user = new Core.Entity.User
            {
                Id = 1,
                UserName = "Testuser",
                FirstName = "Test",
                LastName = "User",
                Email = "testuser@gmail.com",
                Password = "testpassword"
            };
            var command = new UpdateUserCommand
            {
                Id = user.Id,
                UserName = "NewTestUser",
                FirstName = "NewTest",
                LastName = "NewUser",
                Email = "newtestuser@gmail.com",
                Password = "newtestpassword"
            };
            _userRepository.Setup(x => x.GetUserById(user.Id)).ReturnsAsync(user);


            var result = await _handler.Handle(command, CancellationToken.None);


            Assert.NotNull(result);
            Assert.AreEqual(user.Id, result.Id);
            Assert.AreEqual(command.UserName, result.UserName);
            Assert.AreEqual(command.FirstName, result.FirstName);
            Assert.AreEqual(command.LastName, result.LastName);
            Assert.AreEqual(command.Email, result.Email);
            Assert.AreEqual(command.Password, result.Password);
            _userRepository.Verify(x => x.UpdateUser(user), Times.Once);
        }
    }
}            