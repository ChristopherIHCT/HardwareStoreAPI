using HardwareStore.Entities;
using HardwareStore.Services.Interfaces;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
namespace HardwareStore.Services.Implementations;

public class FileUploader : IFileUploader
{
    private readonly ILogger<FileUploader> _logger;
    private readonly AppSettings _appsettings;

    public FileUploader(IOptions<AppSettings> options, ILogger<FileUploader> logger)
    {
        _logger = logger;
        _appsettings = options.Value;

    }

    public async Task<string> UploadFileAsync(string? base64Image, string? fileName)
    {
        if (string.IsNullOrEmpty(base64Image) || string.IsNullOrEmpty(fileName))
        {
            return string.Empty;
        }

        try
        {
            var bytes = Convert.FromBase64String(base64Image);

            var path = Path.Combine(_appsettings.StorageConfiguration.Path, fileName);

            // Escribir el archivo localmente
            await using var fileStream = new FileStream(path, FileMode.Create);
            await fileStream.WriteAsync(bytes, 0, bytes.Length);

            // Verificar la URL pública generada
            var publicUrl = $"{_appsettings.StorageConfiguration.PublicUrl}/{fileName}";

            _logger.LogInformation("Se subió correctamente la imagen con la URL pública: {PublicImageUrl}", publicUrl);

            return publicUrl;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error al subir el archivo {FileName}: {ErrorMessage}", fileName, ex.Message);
            return string.Empty;
        }
    }

}