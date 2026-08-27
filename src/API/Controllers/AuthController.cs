using Microsoft.AspNetCore.Mvc;
using SpaceReservationSystem.Application.Features.Auth;

namespace SpaceReservationSystem.API.Controllers;

[ApiController]
[Route("api/auth")]
public class AuthController(AuthService authService) : ControllerBase
{
    [HttpPost("register")]
    public async Task<IActionResult> Register([FromBody] RegisterRequest request, CancellationToken ct)
    {
        var result = await authService.RegisterAsync(request, ct);
        return result.IsSuccess
            ? CreatedAtAction(nameof(Register), new{ id = result.Value.UserId }, result.Value)
            : BadRequest(new {result.Error.Code, result.Error.Description});
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login([FromBody] LoginRequest request, CancellationToken ct)
    {
        var result = await authService.LoginAsync(request, ct);
        return result.IsSuccess
            ? Ok(result.Value)
            : Unauthorized(new {result.Error.Code, result.Error.Description});
    }
}