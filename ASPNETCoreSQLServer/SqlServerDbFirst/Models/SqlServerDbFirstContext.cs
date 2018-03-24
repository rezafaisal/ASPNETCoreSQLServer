using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace SqlServerDbFirst.Models
{
    public partial class SqlServerDbFirstContext : DbContext
    {
        public virtual DbSet<GuestBook> GuestBook { get; set; }

        public SqlServerDbFirstContext(DbContextOptions<SqlServerDbFirstContext> options) : base(options)
        {
        }

        //        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        //        {
        //            if (!optionsBuilder.IsConfigured)
        //            {
        //#warning To protect potentially sensitive information in your connection string, you should move it out of source code. See http://go.microsoft.com/fwlink/?LinkId=723263 for guidance on storing connection strings.
        //                optionsBuilder.UseSqlServer(@"Server=.\SQLEXPRESS;Database=SqlServerDbFirst;Trusted_Connection=True;");
        //            }
        //        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<GuestBook>(entity =>
            {
                entity.Property(e => e.Email).HasColumnType("nchar(255)");

                entity.Property(e => e.Message).HasColumnType("text");

                entity.Property(e => e.Name).HasColumnType("nchar(50)");
            });
        }
    }
}
