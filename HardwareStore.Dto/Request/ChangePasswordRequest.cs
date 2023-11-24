namespace HardwareStore.Dto.Request;

public record ChangePasswordRequest(string OldPassword, string NewPassword);