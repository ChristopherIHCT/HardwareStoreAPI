namespace HardwareStore.Dto.Request;

public class SearchBase
{
    public int Page { get; set; } = 1;
    public int Rows { get; set; } = 10;
}