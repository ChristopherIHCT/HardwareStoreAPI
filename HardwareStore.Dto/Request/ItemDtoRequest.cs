
namespace HardwareStore.Dto.Request
{
   public class ItemDtoRequest
    {
        public string ItemCode { get; set; } = string.Empty;
        public string ItemName { get; set; } = string.Empty;
        public string BarCode { get; set; } = string.Empty;
        public int CategoryId { get; set; }
        public double Stock { get; set; }
        public double Price { get; set; }
        public string? ImageUrl { get; set; }

        public string? Base64Image { get; set; }
        public string? FileName { get; set; }
    }
}
