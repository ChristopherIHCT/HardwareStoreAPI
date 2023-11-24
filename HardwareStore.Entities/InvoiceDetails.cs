using System.ComponentModel.DataAnnotations.Schema;

namespace HardwareStore.Entities
{
    public class InvoiceDetails : EntityBase
    {
        public int InvoiceId { get; set; }
        public int ItemId { get; set; }

        public int Quantity { get; set; }

        [ForeignKey("InvoiceId")]
        public Invoices Invoice { get; set; } = default!;

        [ForeignKey("ItemId")]
        public Items Item { get; set; } = default!;
    }
}
