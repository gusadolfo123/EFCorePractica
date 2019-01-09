namespace EFdNorthWind.DAL
{
    using EFdNorthWind.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Helpers;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;

    public class EFNorthWindContext: DbContext
    {

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = HelperConfiguration.GetAppConfiguration().ConnectionString;
            optionsBuilder.UseSqlServer(connectionString);

            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Configuracion Entidades
            modelBuilder.Entity<Category>()
                .Property(c => c.CategoryName)
                .HasMaxLength(15)
                .IsRequired();

            modelBuilder.Entity<Product>()
                .Property(p => p.ProductName)
                .HasMaxLength(40)
                .IsRequired();

            modelBuilder.Entity<Log>
                (
                    lg => 
                    {
                        lg.Property(l => l.DateTime)
                            .HasDefaultValueSql("GETDATE()");

                        lg.Property(l => l.Type)
                            .HasConversion(new EnumToStringConverter<LogType>())
                            .HasMaxLength(20);
                    }
                );

            base.OnModelCreating(modelBuilder);
        }
    }
}
