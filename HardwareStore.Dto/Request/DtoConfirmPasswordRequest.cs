using System.ComponentModel.DataAnnotations;

namespace HardwareStore.Dto.Request;

public class DtoConfirmPasswordRequest
{
    [EmailAddress]
    public string Email { get; set; } = default!;

    [Required]
    public string Token { get; set; } = default!;

    [Required]
    public string NewPassword { get; set; } = default!;

    [Compare(nameof(NewPassword))]
    public string ConfirmPassword { get; set; } = default!;
}