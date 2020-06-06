using Ecommerce.Entities.Sql;
using Ecommerce.Models;
using System;

namespace Ecommerce.Helpers
{
    public static class TestFactoryHelpers
    {
        public static User CreateTestUser(long userId = default(long), bool isDeleted = false, string emailAddress = "test@test.com")
        {
            return new User
            {
                UserId = userId,
                EmailAddress = emailAddress,
                FirstName = "test",
                LastName = "test",
                Password = "password",
                CreatedOn = DateTime.UtcNow,
                IsDeleted = isDeleted
            };
        }

        public static SignInModel CreateTestSignInModel(string emailAddress = "test@test.com", string password = "password")
        {
            return new SignInModel
            {
                EmailAddress = emailAddress,
                Password = password
            };
        }

        public static CreateUserModel CreateTestCreateUserModel(string emailAddress = "test@test.com", string firstName = "test", string lastName = "test", string password = "password")
        {
            return new CreateUserModel
            {
                EmailAddress = emailAddress,
                FirstName = firstName,
                LastName = lastName,
                Password = password
            };
        }
    }
}