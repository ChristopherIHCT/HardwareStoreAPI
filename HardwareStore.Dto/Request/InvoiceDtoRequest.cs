namespace HardwareStore.Dto.Request
{
    public class InvoiceDtoRequest
    {
        public string? DocNum { get; set; }
        public int CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public double Total { get; set; }
        public List<InvoiceDetailsDtoRequest>? invoiceDetails { get; set; }
    }
}
