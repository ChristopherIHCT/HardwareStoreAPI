using System.Reflection;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Conventions;
using HardwareStore.Entities;

namespace HardwareStore.Persistence
{
    public class HardwareStoreDbContext : DbContext
    {
        public HardwareStoreDbContext(DbContextOptions<HardwareStoreDbContext> options)
        : base(options)
        {

        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InvoiceDetails>()
                .HasOne(d => d.Invoice)
                .WithMany()
                .HasForeignKey(d => d.InvoiceId);

            modelBuilder.Entity<InvoiceDetails>()
                .HasOne(d => d.Item)
                .WithMany()
                .HasForeignKey(d => d.ItemId);

            base.OnModelCreating(modelBuilder);
            modelBuilder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());

            modelBuilder.Entity<HardwareStoreUserIdentity>(e => e.ToTable("Usuario"));
            modelBuilder.Entity<IdentityRole>(e => e.ToTable("Rol"));

            // Configuración para IdentityUserRole<string>
            modelBuilder.Entity<IdentityUserRole<string>>().ToTable("UsuarioRol").HasKey(p => new { p.UserId, p.RoleId });
        }

        protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
        {
            base.ConfigureConventions(configurationBuilder);

            // Con esto eliminamos el Cascade como metodo principal para la eliminacion de registros
            configurationBuilder.Conventions.Remove(typeof(CascadeDeleteConvention));
        }
    }
}