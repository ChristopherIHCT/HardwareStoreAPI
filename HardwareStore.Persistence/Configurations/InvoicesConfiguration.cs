using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HardwareStore.Entities;

namespace HardwareStore.Persistence.Configurations;

public class InvoicesConfiguration : IEntityTypeConfiguration<Invoices>
{
    public void Configure(EntityTypeBuilder<Invoices> builder)
    {

        builder.Property(p => p.DocNum)
            .IsUnicode(false)
            .HasMaxLength(20);

        builder.Property(p => p.SaleDate)
            .HasColumnType("date")
            .HasDefaultValueSql("GETDATE()");

    }
}