namespace EFdNorthWind.DAL
{
    using EFdNorthWind.Entities;
    using Microsoft.EntityFrameworkCore;
    using System;
    using System.Collections.Generic;
    using System.Text;
    using Helpers;
    using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
    using Microsoft.Extensions.Logging;
    using Microsoft.Extensions.DependencyInjection;

    public class EFNorthWindContext: DbContext
    {

        // Propiedad para log solo mostrara mensajes segun lo indicado
        // singleton para que no se cree cada ves que instancian el contexto y evitar fugas de memoria
        public static readonly ILoggerFactory loggerFactory = 
            new ServiceCollection().AddLogging(builder => 
                builder.AddDebug().AddFilter(Level => Level == LogLevel.Information)
            ).BuildServiceProvider().GetService<ILoggerFactory>();

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            var connectionString = HelperConfiguration.GetAppConfiguration().ConnectionString;
            optionsBuilder.UseSqlServer(connectionString)
                .UseLoggerFactory(loggerFactory) // proveedor de log
                .EnableSensitiveDataLogging(); // para poder ver los valores que se hacen en un insert

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
        }
    }
}
