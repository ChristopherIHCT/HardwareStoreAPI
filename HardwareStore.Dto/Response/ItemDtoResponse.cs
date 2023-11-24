using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HardwareStore.Dto.Response
{
    public class ItemDtoResponse
    {
        public int Id { get; set; }
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public string BarCode { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public double Stock { get; set; }
        public string? ImageUrl { get; set; }

    }
}
