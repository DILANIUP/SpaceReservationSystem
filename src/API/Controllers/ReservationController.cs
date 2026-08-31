using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SpaceReservationSystem.Application.Features.Reservations;

namespace SpaceReservationSystem.API.Controllers;

[ApiController]
[Route("api/reservation")]
[Authorize]
public class ReservationController(ReservationService reservationService) : ControllerBase
{
    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateReservationRequest request, CancellationToken ct)
    {
        var result = await reservationService.CreateAsync(request, GetUserId(), ct);
        return result.IsSuccess
            ? CreatedAtAction(nameof(GetById), new { id = result.Value.Id}, result.Value)
            : BadRequest(new {result.Error.Code, result.Error.Description});
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await reservationService.GetByIdAsync(id, ct);
        return result.IsSuccess
            ? Ok(result.Value)
            : NotFound(new {result.Error.Code, result.Error.Description});
    }

    private Guid GetUserId() =>
        Guid.Parse(User.FindFirstValue(JwtRegisteredClaimNames.Sub)!);
}