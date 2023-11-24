using Microsoft.EntityFrameworkCore;
using HardwareStore.Persistence;
using HardwareStore.Repositories;
using HardwareStore.Services.Implementations;
using HardwareStore.Services.Interfaces;
using HardwareStore.Services.Profiles;
using Microsoft.EntityFrameworkCore.Diagnostics;
using HardwareStore.Entities;
using Serilog;
using Serilog.Events;
using System.Text;
using HardwareStore.Repositories;

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

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();
app.UseStaticFiles();

app.Run();
