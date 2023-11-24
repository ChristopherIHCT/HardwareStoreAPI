using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using HardwareStore.Dto.Request;
using HardwareStore.Services.Interfaces;
using System.Security.Claims;

namespace Music_Store.Controllers;

[ApiController]
[Route("api/[controller]/[action]")]
public class UsersController : ControllerBase
{
    private readonly IUserService _service;

    public UsersController(IUserService service)
    {
        _service = service;
    }

    [HttpPost]
    public async Task<IActionResult> Login(LoginDtoRequest request)
    {
        var response = await _service.LoginAsync(request);

        return response.Success ? Ok(response) : Unauthorized(response);
    }

    [HttpPost]
    public async Task<IActionResult> Register([FromBody] RegisterDtoRequest request)
    {
        var response = await _service.RegisterAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    public async Task<IActionResult> RequestTokenToResetPassword(DtoResetPasswordRequest request)
    {
        var response = await _service.RequestTokenToResetPasswordAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    public async Task<IActionResult> ResetPassword(DtoConfirmPasswordRequest request)
    {
        var response = await _service.ResetPasswordAsync(request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

    [HttpPost]
    [Authorize]
    public async Task<IActionResult> ChangePassword(ChangePasswordRequest request)
    {
        // Aqui recupero el correo del usuario autenticado.
        var email = HttpContext.User.Claims.First(p => p.Type == ClaimTypes.Email).Value;

        var response = await _service.ChangePasswordAsync(email, request);

        return response.Success ? Ok(response) : BadRequest(response);
    }

}