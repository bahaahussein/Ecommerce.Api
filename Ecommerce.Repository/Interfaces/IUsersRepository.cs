using Ecommerce.Entities.Sql;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository.Interfaces
{
    public interface IUsersRepository
    {
        User AddUser(User user);
        Task<User> GetUserById(long id);
        Task<User> GetUserByEmailAddressAndPassword(string emailAddress, string password);
    }
}
