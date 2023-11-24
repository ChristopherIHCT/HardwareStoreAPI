using Microsoft.AspNetCore.Mvc;
using HardwareStore.Dto.Request;
using HardwareStore.Services.Interfaces;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace Music_Store.Controllers;

[ApiController]
[Route("api/[controller]")]
public class InvoicesController : ControllerBase
{
    private readonly IInvoiceService _service;
    private readonly ILogger<InvoicesController> _logger;

    public InvoicesController(IInvoiceService service, ILogger<InvoicesController> logger)
    {
        _service = service;
        _logger = logger;
    }

    [HttpPost]
    public async Task<IActionResult> Post([FromBody]InvoiceDtoRequest request)
    {
        // Aqui recupero el correo del usuario autenticado.
        var email = "prueba@gmail.com";
        //_logger.LogInformation("Id del requester es: {ConnectionId}", HttpContext.Connection.Id);
        var response = await _service.AddAsync(email, request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _service.FindByIdAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }

}