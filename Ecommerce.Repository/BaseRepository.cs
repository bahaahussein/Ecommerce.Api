using Ecommerce.SqlData;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.Repository
{
    public abstract class BaseRepository
    {
        protected readonly SqlDbContext _context;

        public BaseRepository(SqlDbContext context)
        {
            _context = context;
        }
    }
}
