using Ecommerce.Repository.Interfaces;
using Ecommerce.SqlData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public class UnitOfWork : BaseRepository, IUnitOfWork
    {
        public UnitOfWork(SqlDbContext context) : base(context)
        {}

        public async Task<int> SaveChangesAsync()
        {
            return await _context.SaveChangesAsync();
        }
    }
}
