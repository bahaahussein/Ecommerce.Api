using Ecommerce.Entites.Sql;
using Ecommerce.Repository.Interfaces;
using Ecommerce.SqlData;
using Microsoft.EntityFrameworkCore;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class UsersRepository : BaseRepository, IUsersRepository
    {
        public UsersRepository(SqlDbContext context) : base(context)
        {}

        public User AddUser(User user)
        {
            user.CreatedOn = DateTime.UtcNow;
            var addedUser = _context.Users.Add(user);
            return addedUser.Entity;
        }

        public async Task<User> GetUserById(long id)
        {
            var user = await _context.Users.FirstOrDefaultAsync(x => x.UserId == id);
            return user;
        }

        public async Task<User> GetUserByEmailAddressAndPassword(string emailAddress, string password)
        {
            var user = await _context.Users
                .FirstOrDefaultAsync(x => !x.IsDeleted && x.EmailAddress == emailAddress && x.Password == password);
            return user;
        }
    }
}