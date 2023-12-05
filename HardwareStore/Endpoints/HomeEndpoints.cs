using HardwareStore.Dto.Request;
using HardwareStore.Services.Interfaces;

namespace HardwareStore.Endpoints;

public static class HomeEndpoints
{
    public static void MapHomeEndpoints(this IEndpointRouteBuilder routes)
    {
        routes.MapGet("/api/Home", async (ICategoryService categoryService, CancellationToken cancellationToken) =>
        {
            var categories = await categoryService.ListAsync();

            return Results.Ok(new
            {
                Categories = categories.Data,
                Success = true
            });
        }).WithDescription("Permite mostrar los endpoints de la pagina principal")
            .WithOpenApi();
    }
}