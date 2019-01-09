namespace EFdNorthWind.DAL
{
    using EFdNorthWind.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Helpers;

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
            // validacion de entidades


            base.OnModelCreating(modelBuilder);
        }
    }
}
