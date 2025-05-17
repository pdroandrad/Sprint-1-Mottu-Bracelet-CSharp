using Microsoft.EntityFrameworkCore;
using MottuBracelet.Model;

namespace MottuBracelet.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<Patio> Patio { get; set; }
        public DbSet<Dispositivo> Dispositivo { get; set; }
        public DbSet<Moto> Moto { get; set; }
        public DbSet<HistoricoPatio> HistoricoPatio { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Moto>().ToTable("MOTO_NET");
            modelBuilder.Entity<Patio>().ToTable("PATIO_NET");
            modelBuilder.Entity<Dispositivo>().ToTable("DISPOSITIVO_NET");
            modelBuilder.Entity<HistoricoPatio>().ToTable("HISTORICOPATIO_NET");

            modelBuilder.Entity<Moto>()
                .HasOne(m => m.Dispositivo)
                .WithOne(d => d.Moto)
                .HasForeignKey<Dispositivo>(d => d.MotoId)
                .OnDelete(DeleteBehavior.SetNull);

            modelBuilder.Entity<Patio>()
                .OwnsOne(p => p.Endereco);
        }
    }
}