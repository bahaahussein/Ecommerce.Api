using Ecommerce.Entities.Sql;
using Ecommerce.Models;

namespace Ecommerce.AutoMapper.Profiles
{
    public class UserProfile : BaseProfile
    {
        public UserProfile()
        {
            CreateMap<CreateUserModel, User>();
            CreateMap<User, UserReturnModel>();
        }
    }
}
