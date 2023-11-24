namespace HardwareStore.Dto.Request;

public class CategoryDtoRequest
{
    public string Name { get; set; } = default!;
    public bool Status { get; set; }

    public string? ImageUrl { get; set; }

    public string? Base64Image { get; set; }
    public string? FileName { get; set; }
}