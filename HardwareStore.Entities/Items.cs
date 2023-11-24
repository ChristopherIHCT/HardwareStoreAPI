using HardwareStore.Entities;

namespace HardwareStore.Entities
{
    public class Items : EntityBase
    {
        public string ItemCode { get; set; } = default!;
        public string ItemName { get; set; } = default!;
        public string BarCode { get; set; } = default!;

        public double Price { get; set; } = default!;
        public Category Category { get; set; } = default!;
        public int CategoryId { get; set; }

        public double Stock { get; set; }
        public string? ImageUrl { get; set; }
    }
}
