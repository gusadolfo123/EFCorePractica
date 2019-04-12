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
                builder.AddDebug().AddFilter((Category, Level) => Level == LogLevel.Information && Category == DbLoggerCategory.Database.Command.Name)
            ).BuildServiceProvider().GetService<ILoggerFactory>();

        // propiedad apra exponer mensajes de log del proveedor creado
        public List<string> LogMessages { get; set; } = new List<string>();

        public DbSet<Category> Categories { get; set; }
        public DbSet<Product> Products { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // condiguracion de proveedor personalizado
            loggerFactory.AddProvider(new UpdateDeleteLoggerProvider(LogMessages));

            var connectionString = HelperConfiguration.GetAppConfiguration().ConnectionString;
            optionsBuilder.UseSqlServer(connectionString)
                .UseLoggerFactory(loggerFactory) // proveedor de log
                .EnableSensitiveDataLogging(); // para poder ver los valores que se hacen en un insert

        }

        public override int SaveChanges()
        {
            // cada que se lance un savechanges tendra los comandos que tiene ese savechanges
            LogMessages.Clear();
            return base.SaveChanges();
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

            /*  
               implementacion de control de concurrencia
               de esta manera si se cambia la propiedad CategoryName a la ves que otro usuario realizo un cambio a la misma
               propiedad, se generara una excepcion de tipo DbUpdateCurrencyException
            */ 
            modelBuilder.Entity<Category>()
                .Property(c => c.CategoryName)
                .IsConcurrencyToken();

            // se realiza asignacion de propiedad timestan para que funcione como token de concurrencia
            modelBuilder.Entity<Category>()
                .Property<byte[]>("Timestamp")
                .IsRowVersion();

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
