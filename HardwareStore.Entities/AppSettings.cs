#nullable disable

namespace HardwareStore.Entities;

public class AppSettings
{
    public StorageConfiguration StorageConfiguration { get; set; }

    public Jwt Jwt { get; set; }

    public SmtpConfiguration SmtpConfiguration { get; set; } = default!;
}

public class Jwt
{
    public string SecretKey { get; set; }
    public string Audience { get; set; }
    public string Issuer { get; set; }
}

public class StorageConfiguration
{
    public string PublicUrl { get; set; }
    public string Path { get; set; }
}

public class SmtpConfiguration
{
    public string UserName { get; set; } = default!;
    public string Server { get; set; } = default!;
    public string Password { get; set; } = default!;
    public int PortNumber { get; set; }
    public string FromName { get; set; } = default!;
    public bool EnableSsl { get; set; }
}