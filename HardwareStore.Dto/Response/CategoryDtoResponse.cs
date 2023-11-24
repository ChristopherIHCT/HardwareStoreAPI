namespace HardwareStore.Dto.Response;

public class CategoryDtoResponse
{
    public int Id { get; set; }
    public string Name { get; set; } = default!;

    public string ImageUrl { get; set; } = default!;
    public bool Status { get; set; }
}