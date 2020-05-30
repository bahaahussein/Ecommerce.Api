using Ecommerce.Entites.Sql;
using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Helpers
{
    public static class TestFactoryHelpers
    {
        public static User CreateTestUser(bool isDeleted = false, string emailAddress = "test@test.com")
        {
            return new User
            {
                UserId = 1,
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