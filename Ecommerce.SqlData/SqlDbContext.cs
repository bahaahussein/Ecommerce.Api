using Ecommerce.Entities.Sql;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ecommerce.SqlData
{
    public class SqlDbContext : DbContext
    {
        public SqlDbContext() : base() { }

        public SqlDbContext(DbContextOptions<SqlDbContext> options) : base(options)
        {}

        public DbSet<User> Users { get; set; }

        //protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //{
        //    optionsBuilder.UseSqlServer(_settings.ConnectionString);
        //}

        protected override void OnModelCreating(ModelBuilder builder)
        {
            builder.Entity<User>()
                .HasIndex(u => u.EmailAddress)
                .IsUnique();
        }
    }
}
