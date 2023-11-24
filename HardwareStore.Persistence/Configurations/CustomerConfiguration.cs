using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HardwareStore.Entities;

namespace HardwareStore.Persistence.Configurations;

public class CustomerConfiguration : IEntityTypeConfiguration<Customer>
{
    public void Configure(EntityTypeBuilder<Customer> builder)
    {
        builder.Property(p => p.Email)
            .HasMaxLength(200)
            .IsUnicode(false);

        builder.Property(p => p.FullName)
            .HasMaxLength(200);

    }
}