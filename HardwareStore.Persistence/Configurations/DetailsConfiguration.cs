using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using HardwareStore.Entities;

namespace HardwareStore.Persistence.Configurations;

public class DetailsConfiguration : IEntityTypeConfiguration<InvoiceDetails>
{
    public void Configure(EntityTypeBuilder<InvoiceDetails> builder)
    {

    }
}