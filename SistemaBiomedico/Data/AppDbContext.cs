using SistemaBiomedico.Models;
using Microsoft.AspNetCore.Builder.Extensions;
using Microsoft.EntityFrameworkCore;

namespace SistemaBiomedico.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {

        }
        public DbSet<Usuario> Usuarios { get; set; }
        public DbSet<Rol> Roles { get; set; }
        public DbSet<Areas> Areas { get; set; }
        public DbSet<Equipo> Equipos { get; set; }
        public DbSet<Ticket> Tickets { get; set; }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Usuario>()
                .HasOne(u => u.Roles)
                .WithMany(r => r.Usuarios)
                .HasForeignKey(u => u.RolId);
            modelBuilder.Entity<Equipo>()
                .HasOne(e => e.Areas)
                .WithMany(a => a.Equipos)
                .HasForeignKey(e => e.IdAreas);
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Equipo)
                .WithMany(e => e.Tickets)
                .HasForeignKey(t => t.IdEquipo);
            modelBuilder.Entity<Ticket>()
                .HasOne(t => t.Usuario)
                .WithMany(u => u.Tickets)
                .HasForeignKey(t => t.IdUsuario);

            modelBuilder.Entity<Areas>().HasData(
                new Areas { IdAreas = 1, NombreArea = "Cardiología" },
                new Areas { IdAreas = 2, NombreArea = "Radiología" },
                new Areas { IdAreas = 3, NombreArea = "Traumatología" }
            );
            modelBuilder.Entity<Rol>().HasData(
                new Rol { IdRol = 1, Nombre = "Administrador" },
                new Rol { IdRol = 2, Nombre = "Tecnico" },
                new Rol { IdRol = 3, Nombre = "Operativo" }
            );

            modelBuilder.Entity<Usuario>().HasData(
                new Usuario { idUsuario = 1, NameUser = "Admin", Email = "admin@gmail.com", Password = "123456", RolId = 1 },
                new Usuario { idUsuario = 2, NameUser = "Tecnico", Email = "tecnico@gmail.com", Password = "123456", RolId = 2 },
                new Usuario { idUsuario = 3, NameUser = "Operativo", Email = "operativo@gmail.com", Password = "123456", RolId = 3 }
            );
            base.OnModelCreating(modelBuilder);
        }
    }
}
