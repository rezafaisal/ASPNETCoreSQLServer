using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.EntityFrameworkCore;

namespace SqlServerCodeFirst.Models
{
    public class SqlServerCodeFirstContext : DbContext
    {
        public virtual DbSet<GuestBook> GuestBooks { get; set; }

        public SqlServerCodeFirstContext(DbContextOptions<SqlServerCodeFirstContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuestBook>().HasKey(e => new { e.Id });
        }

    }
}
