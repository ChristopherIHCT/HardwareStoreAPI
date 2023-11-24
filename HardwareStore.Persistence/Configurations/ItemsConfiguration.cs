using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HardwareStore.Entities;

namespace HardwareStore.Persistence.Configurations;

public class ItemsConfiguration : IEntityTypeConfiguration<Items>
{
    public void Configure(EntityTypeBuilder<Items> builder)
    {
        builder.Property(p => p.ItemCode)
            .HasMaxLength(100);
        
        builder.Property(p => p.ItemName)
            .HasMaxLength(200);
        
        builder.Property(p => p.Stock)
            .HasMaxLength(200);

        builder.Property(p => p.ImageUrl)
            .IsUnicode(false)
            .HasMaxLength(1000);

        builder.HasIndex(p => p.ItemName);

    }
}