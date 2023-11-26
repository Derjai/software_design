using Microsoft.EntityFrameworkCore.Storage;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Read.Models;

namespace Read.DBContext
{
    public class PersonaDbContext : DbContext
    {
        public PersonaDbContext(DbContextOptions<PersonaDbContext> dbContextOptions) : base(dbContextOptions)
        {
            try
            {
#pragma warning disable IDE0019 // Usar coincidencia de patrones
                var databaseCreator = Database.GetService<IDatabaseCreator>() as RelationalDatabaseCreator;
#pragma warning restore IDE0019 // Usar coincidencia de patrones
                if (databaseCreator != null)
                {
                    if (!databaseCreator.CanConnect()) databaseCreator.Create();
                    if (!databaseCreator.HasTables()) databaseCreator.CreateTables();
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }
        public DbSet<Persona> Personas { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Persona>()
                .Property(p => p.Foto)
                .HasColumnType("longblob");
        }
    }
}
