using Ecommerce.Api.Controllers;
using Ecommerce.Helpers;
using Ecommerce.Models;
using Ecommerce.Models.Enums;
using Ecommerce.Service.Interfaces;
using Moq;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace ECommerce.Api.UnitTests
{
    public class UsersControllerTests : ControllerTestsBase
    {
        private readonly Mock<IUsersService> _usersServiceMock;
        private readonly UsersController _usersController;
        public UsersControllerTests()
        {
            _usersServiceMock = new Mock<IUsersService>();
            _usersController = new UsersController(_usersServiceMock.Object);
        }

        [Fact]
        public void CreateUserModel_NullEmailAddress_ShouldBeInvalidModelState()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(emailAddress: null);

            var isValid = IsModelStateValid(createUserModel);

            Assert.False(isValid);
        }

        [Fact]
        public void CreateUserModel_NullFirstName_ShouldBeInvalidModelState()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(firstName: null);

            var isValid = IsModelStateValid(createUserModel);

            Assert.False(isValid);
        }

        [Fact]
        public void CreateUserModel_NullLastName_ShouldBeInvalidModelState()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(lastName: null);

            var isValid = IsModelStateValid(createUserModel);

            Assert.False(isValid);
        }

        [Fact]
        public void CreateUserModel_NullPassword_ShouldBeInvalidModelState()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(password: null);

            var isValid = IsModelStateValid(createUserModel);

            Assert.False(isValid);
        }

        [Fact]
        public void CreateUserModel_EmptyEmailAddress_ShouldBeInvalidModelState()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(emailAddress: string.Empty);

            var isValid = IsModelStateValid(createUserModel);

            Assert.False(isValid);
        }

        [Fact]
        public void CreateUserModel_FirstNameLengthLessThan3Characters_ShouldBeInvalidModelState()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(firstName: "ab");

            var isValid = IsModelStateValid(createUserModel);

            Assert.False(isValid);
        }

        [Fact]
        public void CreateUserModel_FirstNameLengthMoreThan20Characters_ShouldBeInvalidModelState()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(
                firstName: new string('a', 21));

            var isValid = IsModelStateValid(createUserModel);

            Assert.False(isValid);
        }

        [Fact]
        public void CreateUserModel_LastNameLengthLessThan3Characters_ShouldBeInvalidModelState()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(lastName: "ab");

            var isValid = IsModelStateValid(createUserModel);

            Assert.False(isValid);
        }

        [Fact]
        public void CreateUserModel_LastNameLengthMoreThan20Characters_ShouldBeInvalidModelState()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(
                lastName: new string('a', 21));

            var isValid = IsModelStateValid(createUserModel);

            Assert.False(isValid);
        }

        [Fact]
        public void CreateUserModel_PasswordLengthLessThan8Characters_ShouldBeInvalidModelState()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(password: "1234567");

            var isValid = IsModelStateValid(createUserModel);

            Assert.False(isValid);
        }

        [Fact]
        public void CreateUserModel_InvalidEmail_ShouldBeInvalidModelState()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(emailAddress: "invalidEmail");

            var isValid = IsModelStateValid(createUserModel);

            Assert.False(isValid);
        }

        [Fact]
        public void CreateUserModel_PasswordLengthLessThanEightCharacters_ShouldBeInvalidModelState()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(password: "1234567");

            var isValid = IsModelStateValid(createUserModel);

            Assert.False(isValid);
        }

        [Fact]
        public void CreateUserModel_ValidState_ShouldBeValidModelState()
        {
            var createUserModel = TestFactoryHelpers.CreateTestCreateUserModel(
                emailAddress: "test@test.com",
                firstName: "abc",
                lastName: "xyz",
                password: "12345678");

            var isValid = IsModelStateValid(createUserModel);

            Assert.True(isValid);
        }

        [Fact]
        public async Task CreateUser_NonValidModelState_ShouldReturnBadRequest()
        {
            _usersController.AddTestErrorToModelState();

            var result = await _usersController.CreateUser(null);

            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task CreateUser_ValidModel_ShouldReturnUserWithCreatedStatus()
        {
            var model = TestFactoryHelpers.CreateTestCreateUserModel();
            long testUserId = 1;
            _usersServiceMock.Setup(x => x.AddUser(It.IsAny<CreateUserModel>()))
                .ReturnsAsync((CreateUserModel createUserModel) =>
                {
                    return new ServiceResultModel
                    {
                        Data = new UserReturnModel
                        {
                            EmailAddress = createUserModel.EmailAddress,
                            FirstName = createUserModel.FirstName,
                            LastName = createUserModel.LastName,
                            UserId = testUserId
                        },
                        Status = EServiceActionStatus.Created
                    };
                });

            var result = await _usersController.CreateUser(model);

            var value = (ApiReturnModel)result.Value;
            var data = (UserReturnModel)value.Data;
            Assert.Equal(201, result.StatusCode);
            Assert.Equal(model.EmailAddress, data.EmailAddress);
            Assert.Equal(model.FirstName, data.FirstName);
            Assert.Equal(model.LastName, data.LastName);
            Assert.Equal(testUserId, data.UserId);
        }

        [Fact]
        public void SignInModel_NullEmailAddress_ShouldBeInvalidModelState()
        {
            var model = TestFactoryHelpers.CreateTestSignInModel(emailAddress: null);

            var isValid = IsModelStateValid(model);

            Assert.False(isValid);
        }

        [Fact]
        public void SignInModel_NullPassword_ShouldBeInvalidModelState()
        {
            var model = TestFactoryHelpers.CreateTestSignInModel(password: null);

            var isValid = IsModelStateValid(model);

            Assert.False(isValid);
        }

        [Fact]
        public void SignInModel_EmptyEmailAddress_ShouldBeInvalidModelState()
        {
            var model = TestFactoryHelpers.CreateTestSignInModel(emailAddress: string.Empty);

            var isValid = IsModelStateValid(model);

            Assert.False(isValid);
        }

        [Fact]
        public void SignInModel_PasswordLengthLessThan8Characters_ShouldBeInvalidModelState()
        {
            var model = TestFactoryHelpers.CreateTestSignInModel(password: "1234567");

            var isValid = IsModelStateValid(model);

            Assert.False(isValid);
        }

        [Fact]
        public void SignInModel_InvalidEmail_ShouldBeInvalidModelState()
        {
            var model = TestFactoryHelpers.CreateTestSignInModel(emailAddress: "invalidEmail");

            var isValid = IsModelStateValid(model);

            Assert.False(isValid);
        }

        [Fact]
        public void SignInModel_PasswordLengthLessThanEightCharacters_ShouldBeInvalidModelState()
        {
            var model = TestFactoryHelpers.CreateTestSignInModel(password: "1234567");

            var isValid = IsModelStateValid(model);

            Assert.False(isValid);
        }

        [Fact]
        public void SignInModel_ValidState_ShouldBeValidModelState()
        {
            var model = TestFactoryHelpers.CreateTestSignInModel();

            var isValid = IsModelStateValid(model);

            Assert.True(isValid);
        }

        [Fact]
        public async Task SignIn_NonValidModelState_ShouldReturnBadRequest()
        {
            _usersController.AddTestErrorToModelState();

            var result = await _usersController.SignIn(null);

            Assert.Equal(400, result.StatusCode);
        }

        [Fact]
        public async Task SignIn_WrongCredentials_ShouldReturnUnauthorized()
        {
            _usersServiceMock.Setup(x => x.SignIn(It.IsAny<SignInModel>())).
                ReturnsAsync((SignInModel model) => new ServiceResultModel
                {
                    Status = EServiceActionStatus.Unauthorized
                });
            var signInModel = TestFactoryHelpers.CreateTestSignInModel();

            var result = await _usersController.SignIn(signInModel);

            Assert.Equal(401, result.StatusCode);
        }

        [Fact]
        public async Task SignIn_ValidCredentials_ShouldReturnOkStatusWithTheUser()
        {
            var firstNameTest = "firstName";
            var lastNameTest = "lastName";
            long userIdTest = 1;
            _usersServiceMock.Setup(x => x.SignIn(It.IsAny<SignInModel>())).
                ReturnsAsync((SignInModel model) => new ServiceResultModel
                {
                    Data = new UserReturnModel
                    {
                        EmailAddress = model.EmailAddress,
                        FirstName = firstNameTest,
                        LastName = lastNameTest,
                        UserId = userIdTest
                    },
                    Status = EServiceActionStatus.Ok
                });
            var signInModel = TestFactoryHelpers.CreateTestSignInModel();

            var result = await _usersController.SignIn(signInModel);

            var value = (ApiReturnModel)result.Value;
            var data = (UserReturnModel)value.Data;
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(signInModel.EmailAddress, data.EmailAddress);
            Assert.Equal(firstNameTest, data.FirstName);
            Assert.Equal(lastNameTest, data.LastName);
            Assert.Equal(userIdTest, data.UserId);
        }

        [Fact]
        public async Task GetUserById_NonExistingId_ShouldReturnNotFound()
        {
            _usersServiceMock.Setup(x => x.GetUserById(It.IsAny<long>())).
                ReturnsAsync(() => new ServiceResultModel
                {
                    Status = EServiceActionStatus.NotFound
                });

            var result = await _usersController.GetUserById(-1);

            Assert.Equal(404, result.StatusCode);
        }

        [Fact]
        public async Task GetUserById_ExistingId_ShouldReturnOkStatusWithTheUser()
        {
            var firstNameTest = "firstName";
            var emailAddressTest = "test@test.com";
            var lastNameTest = "lastName";
            long userIdTest = 1;
            _usersServiceMock.Setup(x => x.GetUserById(It.IsAny<long>())).
                ReturnsAsync((long userId) => new ServiceResultModel
                {
                    Data = new UserReturnModel
                    {
                        UserId = userId,
                        EmailAddress = emailAddressTest,
                        FirstName = firstNameTest,
                        LastName = lastNameTest
                    },
                    Status = EServiceActionStatus.Ok
                });

            var result = await _usersController.GetUserById(userIdTest);

            var value = (ApiReturnModel)result.Value;
            var data = (UserReturnModel)value.Data;
            Assert.Equal(200, result.StatusCode);
            Assert.Equal(emailAddressTest, data.EmailAddress);
            Assert.Equal(firstNameTest, data.FirstName);
            Assert.Equal(lastNameTest, data.LastName);
            Assert.Equal(userIdTest, data.UserId);
        }

    }
}