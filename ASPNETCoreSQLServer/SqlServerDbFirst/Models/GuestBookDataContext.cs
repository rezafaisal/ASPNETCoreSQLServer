using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace SqlServerDbFirst.Models
{
    public class GuestBookDataContext: DbContext
    {
        public GuestBookDataContext(DbContextOptions<GuestBookDataContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuestBook>().ToTable("GuestBook");
            modelBuilder.Entity<GuestBook>(entity =>
            {
                entity.Property(e => e.Id).HasColumnName("Id");
                entity.Property(e => e.Email).HasColumnName("Email");
                entity.Property(e => e.Name).HasColumnName("Name");
                entity.Property(e => e.Message).HasColumnName("Message");
            });
            modelBuilder.Entity<GuestBook>().HasKey(e => new { e.Id });
        }
        public virtual DbSet<GuestBook> GuestBooks { get; set; }
    }

}
