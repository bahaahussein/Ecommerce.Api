using Ecommerce.Entites.Sql;
using Ecommerce.Helpers;
using Ecommerce.SqlData;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using Moq;
using System;
using System.Linq;
using System.Threading.Tasks;
using Xunit;

namespace Ecommerce.Repository.UnitTests
{
    public class UsersRepositoryTests : TestWithSqlite
    {
        [Fact]
        public async Task GetUserById_ExistingUserId_ShouldReturnUser()
        {
            long userIdTest = 1;
            var usersRepository = new UsersRepository(DbContext);
            var existingUser = TestFactoryHelpers.CreateTestUser(userId: userIdTest);
            DbContext.Users.Add(existingUser);
            DbContext.SaveChanges();

            var user = await usersRepository.GetUserById(userIdTest);

            Assert.Equal(userIdTest, user.UserId);
        }

        [Fact]
        public async Task GetUserById_NonExistingUserId_ShouldReturnNull()
        {
            long userIdTest = 1;
            var usersRepository = new UsersRepository(DbContext);
            var existingUser = TestFactoryHelpers.CreateTestUser(userId: userIdTest);
            DbContext.Users.Add(existingUser);
            DbContext.SaveChanges();

            var user = await usersRepository.GetUserById(userIdTest + 1);

            Assert.Null(user);
        }
        
        [Fact]
        public async Task GetUserByEmailAddressAndPassword_DeletedUser_ShouldReturnNull()
        {
            var usersRepository = new UsersRepository(DbContext);
            var existingUser = TestFactoryHelpers.CreateTestUser(isDeleted: true);
            
            DbContext.Users.Add(existingUser);
            DbContext.SaveChanges();

            var user = await usersRepository.GetUserByEmailAddressAndPassword("test@test.com", "password");

            Assert.Null(user);
        }

        [Fact]
        public async Task GetUserByEmailAddressAndPassword_ValidCredentialsForNotDeletedUser_ShouldReturnUser()
        {
            var usersRepository = new UsersRepository(DbContext);
            var existingUser = TestFactoryHelpers.CreateTestUser();
            DbContext.Users.Add(existingUser);
            DbContext.SaveChanges();

            var user = await usersRepository.GetUserByEmailAddressAndPassword("test@test.com", "password");

            Assert.Equal(existingUser.UserId, user.UserId);
        }

        [Fact]
        public async Task GetUserByEmailAddressAndPassword_NonExistingEmailAddress_ShouldReturnNull()
        {
            var usersRepository = new UsersRepository(DbContext);
            var existingUser = TestFactoryHelpers.CreateTestUser();
            DbContext.Users.Add(existingUser);
            DbContext.SaveChanges();

            var user = await usersRepository.GetUserByEmailAddressAndPassword("nonExisting@test.com", "password");

            Assert.Null(user);
        }

        [Fact]
        public async Task GetUserByEmailAddressAndPassword_WrongPassword_ShouldReturnNull()
        {
            var usersRepository = new UsersRepository(DbContext);
            var existingUser = TestFactoryHelpers.CreateTestUser();
            DbContext.Users.Add(existingUser);
            DbContext.SaveChanges();

            var user = await usersRepository.GetUserByEmailAddressAndPassword("test@test.com", "wrongPassword");

            Assert.Null(user);
        }

        [Fact]
        public void AddUser_WhenCalled_ShouldPutCreatedOnToUser()
        {
            var usersRepository = new UsersRepository(DbContext);
            var userToAdd = TestFactoryHelpers.CreateTestUser();

            var addedUser = usersRepository.AddUser(userToAdd);

            Assert.NotNull(addedUser.CreatedOn);
        }

        [Fact]
        public void AddUser_WhenCalled_ShouldReturnSameUser()
        {
            var usersRepository = new UsersRepository(DbContext);
            var userToAdd = TestFactoryHelpers.CreateTestUser();

            var addedUser = usersRepository.AddUser(userToAdd);

            Assert.Equal(userToAdd, addedUser);
        }

        [Fact]
        public void AddUser_WhenCalled_ShouldTrackUserInDbContext()
        {
            var usersRepository = new UsersRepository(DbContext);
            var userToAdd = TestFactoryHelpers.CreateTestUser();

            var addedUser = usersRepository.AddUser(userToAdd);

            var trackingEntities = DbContext.ChangeTracker.Entries().Select(x => x.Entity);
            Assert.Contains(addedUser, trackingEntities);
        }
    }
}