using Ecommerce.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Service.Interfaces
{
    public interface IUsersService
    {
        Task<ServiceResultModel> AddUser(CreateUserModel model);
        Task<ServiceResultModel> GetUserById(long id);
        Task<ServiceResultModel> SignIn(SignInModel model);
    }
}
