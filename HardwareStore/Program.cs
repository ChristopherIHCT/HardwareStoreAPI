using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;
using Microsoft.IdentityModel.Tokens;
using HardwareStore.Entities;
using HardwareStore.Persistence;
using HardwareStore.Repositories;
using HardwareStore.Services.Implementations;
using HardwareStore.Services.Interfaces;
using HardwareStore.Services.Profiles;
using Serilog;
using Serilog.Events;
using System.Text;

var corsConfiguration = "HardwareStoreCors";
var builder = WebApplication.CreateBuilder(args);
var logger = new LoggerConfiguration()
    .WriteTo.Console(LogEventLevel.Information)
    .WriteTo.File("..\\log.log", rollingInterval: RollingInterval.Day,
         restrictedToMinimumLevel: LogEventLevel.Warning,
         fileSizeLimitBytes: 4 * 1024)
    .CreateLogger();

builder.Logging.AddSerilog(logger);

// Mapeamos el contenido del archivo appsettings.json a una clase
builder.Services.Configure<AppSettings>(builder.Configuration);

builder.Services.AddCors(setup =>
{
    setup.AddPolicy(corsConfiguration, policy =>
    {
        policy.AllowAnyOrigin(); // Que cualquiera pueda consumir el API
        policy.AllowAnyHeader();
        policy.AllowAnyMethod();
    });
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();

builder.Services.AddDbContext<HardwareStoreDbContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("HardwareStoreDb"));
    options.EnableSensitiveDataLogging();

    options.ConfigureWarnings(configurationBuilder =>
        configurationBuilder.Ignore(CoreEventId.PossibleIncorrectRequiredNavigationWithQueryFilterInteractionWarning));

    options.ConfigureWarnings(x => x.Ignore(CoreEventId.SensitiveDataLoggingEnabledWarning));
});

builder.Services.AddIdentity<HardwareStoreUserIdentity, IdentityRole>(policies =>
{
    policies.Password.RequireDigit = true;
    policies.Password.RequireLowercase = true;
    policies.Password.RequireUppercase = false;
    policies.Password.RequireNonAlphanumeric = true;
    policies.Password.RequiredLength = 6;

    policies.User.RequireUniqueEmail = true;

    // Politica de bloque de cuentas
    policies.Lockout.DefaultLockoutTimeSpan = TimeSpan.FromMinutes(10);
    policies.Lockout.MaxFailedAccessAttempts = 5;
}).AddEntityFrameworkStores<HardwareStoreDbContext>()
    .AddDefaultTokenProviders();

// Transient - Siempre crea una instancia de la clase expuesta
// Scoped - Utiliza la misma instancia por Sesion (en aplicaciones .NET que tengan frontend) - ASP.NET MVC/BLAZOR
// Singleton - Utiliza la misma instancia por toda la aplicacion

builder.Services.AddTransient<ICategoryRepository, CategoryRepository>();
builder.Services.AddTransient<IItemRepository, ItemRepository>();
builder.Services.AddTransient<ICustomerRepository, CustomerRepository>();
builder.Services.AddTransient<IInvoiceDetailsRepository, InvoiceDetailsRepository>();
builder.Services.AddScoped<IInvoiceRepository, InvoiceRepository>();




builder.Services.AddTransient<ICategoryService, CategoryService>();
builder.Services.AddTransient<IItemService, ItemService>();
builder.Services.AddTransient<IInvoiceDetailsRepository, InvoiceDetailsRepository>();
builder.Services.AddScoped<IInvoiceService, InvoiceService>();
builder.Services.AddTransient<IUserService, UserService>();
builder.Services.AddTransient<IEmailService, EmailService>();

if (builder.Configuration.GetSection("StorageConfiguration:PublicUrl").Value!.Contains("core.windows.net"))
{
    builder.Services.AddTransient<IFileUploader, AzureBlobStorageUploader>();
}
else
{
    builder.Services.AddTransient<IFileUploader, FileUploader>();
}

builder.Services.AddAutoMapper(config =>
{
    config.AddProfile<CategoryProfile>();
    config.AddProfile<InvoiceProfile>();
    config.AddProfile<ItemsProfile>();
});

builder.Services.AddAuthentication(x =>
{
    x.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    x.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(x =>
{
    var key = Encoding.UTF8.GetBytes(builder.Configuration["Jwt:SecretKey"] ??
                                     throw new InvalidOperationException("No se configuro el JWT"));

    x.TokenValidationParameters = new TokenValidationParameters
    {
        ValidateIssuer = true,
        ValidateAudience = true,
        ValidateLifetime = true,
        ValidateIssuerSigningKey = true,
        ValidIssuer = builder.Configuration["Jwt:Issuer"],
        ValidAudience = builder.Configuration["Jwt:Audience"],
        IssuerSigningKey = new SymmetricSecurityKey(key)
    };
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseAuthentication();
app.UseAuthorization(); 
app.MapControllers();
app.UseStaticFiles();

// Un scope permite tener en un contexto los servicios inyectados previamente
await using (var scope = app.Services.CreateAsyncScope())
{
    if (app.Environment.IsDevelopment())
    {
        var db = scope.ServiceProvider.GetRequiredService<HardwareStoreDbContext>();
        await db.Database.MigrateAsync(); // Esto ejecuta las migraciones de forma automática.
    }

    // Aqui vamos a ejecutar la creacion del usuario admin y los roles por default
    await UserDataSeeder.Seed(scope.ServiceProvider);
}

app.Run();
