namespace HardwareStore.Services.Interfaces;

public interface IFileUploader
{
    Task<string> UploadFileAsync(string? base64Image, string? fileName);
}