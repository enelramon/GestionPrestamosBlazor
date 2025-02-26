using GestionPrestamos.Models;
using Microsoft.EntityFrameworkCore;

namespace GestionPrestamos.Context;

public class Contexto : DbContext
{
    public Contexto(DbContextOptions<Contexto> options) : base(options) { }

    public virtual DbSet<Deudores> Deudores { get; set; }
    public virtual DbSet<Prestamos> Prestamos { get; set; }
    public virtual DbSet<PrestamosDetalle> PrestamosDetalles { get; set; }
    public virtual DbSet<Cobros> Cobros { get; set; }
    public virtual DbSet<CobrosDetalle> CobrosDetalle { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PrestamosDetalle>()
            .HasOne(pd => pd.Prestamo)
            .WithMany(p => p.PrestamosDetalle)
            .HasForeignKey(pd => pd.PrestamoId)
            .OnDelete(DeleteBehavior.Cascade);

        modelBuilder.Entity<Deudores>()
            .HasMany(d => d.Prestamos)
            .WithOne(p => p.Deudor)
            .HasForeignKey(p => p.DeudorId)
            .OnDelete(DeleteBehavior.Restrict);

        modelBuilder.Entity<Deudores>().HasData(
            new List<Deudores>()
            {
                new()
                {
                    DeudorId = 1,
                    Nombres = "Jose Lopez",
                },
                new()
                {
                    DeudorId = 2,
                    Nombres = "Maria Perez",
                }
            }
        );

        base.OnModelCreating(modelBuilder);
    }
}
