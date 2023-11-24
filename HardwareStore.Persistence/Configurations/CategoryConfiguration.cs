using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HardwareStore.Entities;

namespace HardwareStore.Persistence.Configurations;

public class CategoryConfiguration : IEntityTypeConfiguration<Category>
{
    public void Configure(EntityTypeBuilder<Category> builder)
    {
        // Fluent API

        builder
            .Property(p => p.Name)
            .HasMaxLength(50);

        builder.HasQueryFilter(p => p.Status);
        var fecha = DateTime.Now;

        builder.HasData(new List<Category>
        {
            new() { Id = 1, Name = "Electricidad", CreationDate = fecha},
            new() { Id = 2, Name = "Fontaneria" , CreationDate = fecha},
            new() { Id = 3, Name = "Herramientas" , CreationDate = fecha},
        });
    }
}