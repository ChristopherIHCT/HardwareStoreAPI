namespace HardwareStore.Dto.Response;

public class LoginDtoResponse : BaseResponse
{
    public string FullName { get; set; } = default!;
    public List<string> Roles { get; set; } = default!;
    public string Token { get; set; } = default!;
}