using Market.DAL.Interfaces;
using Market.Domain.Entity;
using Market.Domain.Enum;
using Market.Domain.ViewModels.User;
using Market.Service.Implementations;
using Market.Service.Interfaces;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace Market.Tests
{
    public class UserServiceTests
    {
        private readonly Mock<ILogger<UserService>> _loggerMock;
        private readonly Mock<IBaseRepository<Profile>> _profileRepositoryMock;
        private readonly Mock<IBaseRepository<User>> _userRepositoryMock;

        private readonly IUserService _userService;

        public UserServiceTests()
        {
            _loggerMock = new Mock<ILogger<UserService>>();
            _profileRepositoryMock = new Mock<IBaseRepository<Profile>>();
            _userRepositoryMock = new Mock<IBaseRepository<User>>();

            _userService = new UserService(
                _loggerMock.Object,
                _userRepositoryMock.Object,
                _profileRepositoryMock.Object
            );
        }

        [Fact]
        public async Task CreateAsync_ValidModel_ReturnsSuccessResponse()
        {
            // Arrange
            var userModel = new UserViewModel
            {
                Name = "testuser",
                Role = "Admin",
                Password = "password"
            };

            _userRepositoryMock.Setup(x => x.GetAll()).Returns(CreateMockUserQueryable(null));
            _userRepositoryMock.Setup(x => x.Create(It.IsAny<User>())).Returns(Task.CompletedTask);
            _profileRepositoryMock.Setup(x => x.Create(It.IsAny<Profile>())).Returns(Task.CompletedTask);

            // Act
            var response = await _userService.CreateAsync(userModel);

            // Assert
            Assert.Equal(StatusCode.OK, response.StatusCode);
            Assert.Equal("Користувача додано", response.Description);
            Assert.NotNull(response.Data);
            Assert.Equal(userModel.Name, response.Data.Name);
            Assert.Equal(Role.Admin, response.Data.Role);
            Assert.NotNull(response.Data.Password);
            _userRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            _userRepositoryMock.Verify(x => x.Create(It.IsAny<User>()), Times.Once);
            _profileRepositoryMock.Verify(x => x.Create(It.IsAny<Profile>()), Times.Once);
        }

        [Fact]
        public async Task CreateAsync_UserAlreadyExists_ReturnsErrorResponse()
        {
            // Arrange
            var existingUser = new User { Name = "existinguser" };
            var userModel = new UserViewModel { Name = existingUser.Name };

            _userRepositoryMock.Setup(x => x.GetAll()).Returns(CreateMockUserQueryable(existingUser));

            // Act
            var response = await _userService.CreateAsync(userModel);

            // Assert
            Assert.Equal(StatusCode.UserAlreadyExists, response.StatusCode);
            Assert.Equal("Користувач з таким логіном вже зареєстрований", response.Description);
            Assert.Null(response.Data);
            _userRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            _userRepositoryMock.Verify(x => x.Create(It.IsAny<User>()), Times.Never);
            _profileRepositoryMock.Verify(x => x.Create(It.IsAny<Profile>()), Times.Never);
        }

        [Fact]
        public void GetRoles_ReturnsRolesDictionary()
        {
            // Act
            var response = _userService.GetRoles();

            // Assert
            Assert.Equal(StatusCode.OK, response.StatusCode);
            Assert.NotNull(response.Data);
            Assert.IsType<Dictionary<int, string>>(response.Data);
        }



        [Fact]
        public async Task DeleteUserAsync_ExistingUser_ReturnsSuccessResponse()
        {
            // Arrange
            var userId = 1;
            var existingUser = new User { Id = userId };
            _userRepositoryMock.Setup(x => x.GetAll()).Returns(CreateMockUserQueryable(existingUser));
            _userRepositoryMock.Setup(x => x.Delete(existingUser)).Returns(Task.CompletedTask);

            // Act
            var response = await _userService.DeleteUserAsync(userId);

            // Assert
            Assert.Equal(StatusCode.OK, response.StatusCode);
            Assert.True(response.Data);
            _userRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            _userRepositoryMock.Verify(x => x.Delete(existingUser), Times.Once);
        }

        [Fact]
        public async Task DeleteUserAsync_NonExistingUser_ReturnsErrorResponse()
        {
            // Arrange
            var userId = 1;
            _userRepositoryMock.Setup(x => x.GetAll()).Returns(CreateMockUserQueryable(null));

            // Act
            var response = await _userService.DeleteUserAsync(userId);

            // Assert
            Assert.Equal(StatusCode.UserNotFound, response.StatusCode);
            Assert.False(response.Data);
            _userRepositoryMock.Verify(x => x.GetAll(), Times.Once);
            _userRepositoryMock.Verify(x => x.Delete(It.IsAny<User>()), Times.Never);
        }

        private IQueryable<User> CreateMockUserQueryable(User user)
        {
            var users = new List<User>();
            if (user != null)
                users.Add(user);
            return users.AsQueryable();
        }
    }
}
