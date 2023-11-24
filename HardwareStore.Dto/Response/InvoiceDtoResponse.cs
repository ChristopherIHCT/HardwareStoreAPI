using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStore.Dto.Response
{
    public class InvoiceDtoResponse
    {
        public string? DocNum { get; set; }
        public int CustomerId { get; set; }
        public DateTime SaleDate { get; set; }
        public double Total { get; set; }

        public List<InvoiceDetailsDtoResponse>? InvoiceDetails { get; set; }

    }
}
