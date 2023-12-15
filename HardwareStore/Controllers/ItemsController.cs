using Microsoft.AspNetCore.Mvc;
using HardwareStore.Dto.Request;
using HardwareStore.Services.Interfaces;
using Microsoft.AspNetCore.Authorization;
using HardwareStore.Entities;

namespace HardwareStore.Controllers;
[ApiController]
[Route("api/[controller]")]
public class ItemsController : ControllerBase
{
    private readonly IItemService _service;

    public ItemsController(IItemService service)
    {
        _service = service;
    }

    [HttpGet]
    public async Task<IActionResult> Get([FromQuery] ItemSearch search, CancellationToken cancellationToken = default)
    {
        var response = await _service.ListAsync(search, cancellationToken);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpGet("{id:int}")]
    public async Task<IActionResult> Get(int id)
    {
        var response = await _service.FindByIdAsync(id);

        return response.Success ? Ok(response) : NotFound(response);
    }

    [HttpGet("Category/{categoryId:int}")]
    public async Task<IActionResult> GetByCategory(int categoryId)
    {
        var response = await _service.ListAsyncByCategory(categoryId);

        return response.Success ? Ok(response) : NotFound(response);
    }


    [HttpPost]
   
    public async Task<IActionResult> Post([FromBody] ItemDtoRequest request)
    {
        var response = await _service.AddAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPut("{id:int}")]
    [Authorize(Roles = Constantes.RolAdmin)]
    public async Task<IActionResult> Put(int id, [FromBody] ItemDtoRequest request)
    {
        var response = await _service.UpdateAsync(id, request);
        return response.Success ? Ok(response) : BadRequest(response);
    }


    [HttpDelete("{id:int}")]
    [Authorize(Roles = Constantes.RolAdmin)]
    public async Task<IActionResult> Delete(int id)
    {
        var response = await _service.DeleteAsync(id);
        return Ok(response);
    }
}