
namespace HardwareStore.Entities
{
    public class Invoices : EntityBase
    {
        public string? DocNum { get; set; }
        public Customer Customer { get; set; } = default!;
        public int CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public double Total { get; set; }

    }
}