using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStore.Dto.Response
{
    public class InvoiceDetailsDtoResponse
    {
        public int ItemId { get; set; }
        public string ItemName { get; set; } = string.Empty;
    }
}
