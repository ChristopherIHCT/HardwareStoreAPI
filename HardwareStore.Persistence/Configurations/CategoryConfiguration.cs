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
            new() { Id = 1, Name = "Electricidad",ImageUrl="https://localhost:7000/imagenes//electricidad.jpg", CreationDate = fecha},
            new() { Id = 2, Name = "Fontaneria", ImageUrl="https://localhost:7000/imagenes//fontaneria.jpg", CreationDate = fecha},
            new() { Id = 3, Name = "Herramientas", ImageUrl="https://localhost:7000/imagenes//herramientas.jpg" , CreationDate = fecha},
        });
    }
}