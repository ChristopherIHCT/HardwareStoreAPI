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
    [Authorize]
    public async Task<IActionResult> Post([FromBody]InvoiceDtoRequest request)
    {
        var email = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Email).Value;
        _logger.LogInformation("Id del requester es: {ConnectionId}", HttpContext.Connection.Id);
        var response = await _service.AddAsync(email, request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id:int}")]
    [Authorize]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _service.FindByIdAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }

}