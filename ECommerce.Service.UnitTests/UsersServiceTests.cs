using AutoMapper;
using Ecommerce.Entities.Sql;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Ecommerce.Models.Enums;
using Ecommerce.Repository;
using Ecommerce.Repository.Interfaces;
using Ecommerce.Service;
using Moq;
using System;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Service.UnitTests
{
    public class UsersServiceTests : ServiceTestsBase
    {
        private readonly UsersService _usersService;

        public UsersServiceTests() : base()
        {
            _usersService = new UsersService(_usersRepositoryMock.Object, _unitOfWorkMock.Object, _mapper);
        }

        [Fact]
        public async Task GetUserById_NonExistingUser_ShouldReturnNotFoundStatusAndNullData()
        {
            _usersRepositoryMock.Setup(x => x.GetUserById(It.IsAny<long>())).ReturnsAsync(value: null);

            var result = await _usersService.GetUserById(1);

            Assert.Null(result.Data);
            Assert.Equal(EServiceActionStatus.NotFound, result.Status);
        }

        [Fact]
        public async Task GetUserById_ExistingUser_ShouldReturnOkStatusWithTheUser()
        {
            var user = TestFactoryHelpers.CreateTestUser();
            _usersRepositoryMock.Setup(x => x.GetUserById(1)).ReturnsAsync(user);
            
            var result = await _usersService.GetUserById(1);

            var data = (UserReturnModel)result.Data;
            Assert.Equal(user.UserId, data.UserId);
            Assert.Equal(user.EmailAddress, data.EmailAddress);
            Assert.Equal(EServiceActionStatus.Ok, result.Status);
        }

        [Fact]
        public async Task SignIn_WhenCalled_ShouldLowerCaseEmailAddress()
        {
            var emailAddress = "TEST@TEST.COM";
            var signInModel = TestFactoryHelpers.CreateTestSignInModel(emailAddress: emailAddress);
            _usersRepositoryMock
                .Setup(x =>
                    x.GetUserByEmailAddressAndPassword(emailAddress.ToLower(), It.IsAny<string>()))
                .ReturnsAsync(TestFactoryHelpers.CreateTestUser(emailAddress: emailAddress.ToLower()));

            var result = await _usersService.SignIn(signInModel);

            var data = (UserReturnModel)result.Data;
            Assert.Equal(emailAddress.ToLower(), data.EmailAddress);
            Assert.Equal(EServiceActionStatus.Ok, result.Status);
        }

        [Fact]
        public async Task SignIn_ValidCredentials_ShouldReturnOkWithUserReturnedFromRepository()
        {
            var signInModel = TestFactoryHelpers.CreateTestSignInModel();
            var user = TestFactoryHelpers.CreateTestUser();
            var returnedUserFromRepository = 
            _usersRepositoryMock
                .Setup(x =>
                    x.GetUserByEmailAddressAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(user);

            var result = await _usersService.SignIn(signInModel);

            var data = (UserReturnModel)result.Data;
            Assert.Equal(user.UserId, data.UserId);
            Assert.Equal(user.EmailAddress, data.EmailAddress);
            Assert.Equal(EServiceActionStatus.Ok, result.Status);
        }

        [Fact]
        public async Task SignIn_InvalidCredentials_ShouldReturnUnAuthorizedWithNullObject()
        {
            var signInModel = TestFactoryHelpers.CreateTestSignInModel();
            var returnedUserFromRepository =
            _usersRepositoryMock
                .Setup(x =>
                    x.GetUserByEmailAddressAndPassword(It.IsAny<string>(), It.IsAny<string>()))
                .ReturnsAsync(value: null);

            var result = await _usersService.SignIn(signInModel);

            Assert.Null(result.Data);
            Assert.Equal(EServiceActionStatus.Unauthorized, result.Status);
        }

        [Fact]
        public async Task AddUser_WhenCalled_ShouldLowerCaseEmailAddress()
        {
            var emailAddress = "TEST@TEST.COM";
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(emailAddress: emailAddress);
            
            var result = await _usersService.AddUser(createUserModel);

            var data = (UserReturnModel)result.Data;
            Assert.Equal(emailAddress.ToLower(), data.EmailAddress);
            Assert.Equal(EServiceActionStatus.Created, result.Status);
        }

        [Fact]
        public async Task AddUser_WhenCalled_ShouldReturnOkWithTheAddedUser()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel();

            var result = await _usersService.AddUser(createUserModel);

            var data = (UserReturnModel)result.Data;
            Assert.Equal(createUserModel.EmailAddress, data.EmailAddress);
            Assert.Equal(EServiceActionStatus.Created, result.Status);
        }
    }
}