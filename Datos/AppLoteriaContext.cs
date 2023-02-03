using Datos.Models;
using Microsoft.EntityFrameworkCore;

namespace Datos
{
    public class AppLoteriaContext : DbContext
    {
        public AppLoteriaContext(DbContextOptions<AppLoteriaContext> options) : base(options)
        {

        }

        public DbSet<Carta> Cartas { get; set; }
        public DbSet<Tabla> Tablas { get; set; }
        public DbSet<Ganador> Ganadores { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Carta>().ToTable("Carta");
            modelBuilder.Entity<Tabla>().ToTable("Tabla");
            modelBuilder.Entity<Ganador>().ToTable("Ganador");
        }
    }
}