using System.Reflection;
using Microsoft.EntityFrameworkCore;
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
        }
    }
}