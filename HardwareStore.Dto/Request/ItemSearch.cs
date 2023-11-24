namespace HardwareStore.Dto.Request;

public class ItemSearch : SearchBase
{
    public string? ItemName { get; set; }
        public int? CategoryId { get; set; }
}