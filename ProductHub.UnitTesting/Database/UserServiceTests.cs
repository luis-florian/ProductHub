using Microsoft.EntityFrameworkCore;
using ProductHub.Database.Context;
using ProductHub.Database.Entities;
using ProductHub.Database.Services;
using Xunit;

namespace ProductHub.UnitTesting.Database
{
    public class UserServiceTests
    {
        [Fact]
        public async Task Create_ShouldAddUserToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Create_ShouldAddUserToDatabase")
                .Options;

            using var context = new DBContext(options);
            var userService = new UserService(context);

            var user = new User { Name = "John", Email = "john@example.com" };

            // Act
            var createdUser = await userService.Create(user);

            // Assert
            Assert.NotNull(createdUser);
            Assert.Equal(user.Name, createdUser!.Name);
            Assert.Equal(user.Email, createdUser.Email);
        }

        [Fact]
        public async Task GetById_ShouldReturnUserIfExists()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetById_ShouldReturnUserIfExists")
                .Options;

            using var context = new DBContext(options);
            var userService = new UserService(context);

            var user = new User { Id = 1, Name = "John", Email = "john@example.com" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Act
            var retrievedUser = await userService.GetById(1);

            // Assert
            Assert.NotNull(retrievedUser);
            Assert.Equal(user.Name, retrievedUser!.Name);
            Assert.Equal(user.Email, retrievedUser.Email);
        }

        [Fact]
        public async Task GetAll_ShouldReturnAllUsers()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetAll_ShouldReturnAllUsers")
                .Options;

            using var dbContext = new DBContext(options);

            var users = new List<User>
            {
                new() { Id = 1, Name = "User1", Email = "user1@example.com" },
                new() { Id = 2, Name = "User2", Email = "user2@example.com" }
                // Add more users if needed
            };

            dbContext.Users.AddRange(users);
            dbContext.SaveChanges();

            var userService = new UserService(dbContext);

            // Act
            var result = await userService.GetAll();

            // Assert
            Assert.NotNull(result);
            Assert.Equal(users.Count, result.Count);
            // Add more assertions based on your requirements
        }

        [Fact]
        public async Task Update_ShouldUpdateUser_WhenUserExists()
        {
            // Arrange
            var existingUser = new User { Id = 1, Name = "User1", Email = "user1@example.com" };
            var updatedUser = new User { Id = 1, Name = "UpdatedUser", Email = "updateduser@example.com" };

            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Update_ShouldUpdateUser_WhenUserExists")
                .Options;

            using var dbContext = new DBContext(options);

            dbContext.Users.Add(existingUser);
            dbContext.SaveChanges();

            var userService = new UserService(dbContext);

            // Act
            var result = await userService.Update(updatedUser);

            // Assert
            Assert.NotNull(result);
            Assert.Equal(updatedUser.Name, result.Name);
            Assert.Equal(updatedUser.Email, result.Email);
            // Add more assertions based on your requirements
        }



        [Fact]
        public async Task GetById_ShouldNotReturnUserToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetById_ShouldNotReturnUserToDatabase")
                .Options;

            using var context = new DBContext(options);
            var userService = new UserService(context);

            var user = new User { Id = 1, Name = "John", Email = "john@example.com" };
            context.Users.Add(user);
            await context.SaveChangesAsync();

            // Act
            var retrievedUser = await userService.GetById(2);

            // Assert
            Assert.Null(retrievedUser);
        }
        [Fact]
        public async Task GetAll_ShouldNotReturnUsersToDatabase()
        {
            // Arrange
            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_GetAll_ShouldNotReturnUsersToDatabase")
                .Options;

            using var dbContext = new DBContext(options);

            var userService = new UserService(dbContext);

            // Act
            var result = await userService.GetAll();

            // Assert
            Assert.Empty(result);
            // Add more assertions based on your requirements
        }
        [Fact]
        public async Task Update_ShouldNotUpdateUser_WhenUserNotExists()
        {
            // Arrange
            var existingUser = new User { Id = 1, Name = "User1", Email = "user1@example.com" };
            var updatedUser = new User { Id = 2, Name = "UpdatedUser", Email = "updateduser@example.com" };

            var options = new DbContextOptionsBuilder<DBContext>()
                .UseInMemoryDatabase(databaseName: "TestDB_Update_ShouldNotUpdateUser_WhenUserNotExists")
                .Options;

            using var dbContext = new DBContext(options);

            dbContext.Users.Add(existingUser);
            dbContext.SaveChanges();

            var userService = new UserService(dbContext);

            // Act
            var result = await userService.Update(updatedUser);

            // Assert
            Assert.Null(result);
            // Add more assertions based on your requirements
        }
    }
}
