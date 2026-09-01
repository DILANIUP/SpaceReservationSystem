using Microsoft.AspNetCore.Mvc;
using SpaceReservationSystem.Application.Features.Alert;

namespace SpaceReservationSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class AlertController : ControllerBase
{
    private readonly AlertService _alertService;

    public AlertController(AlertService alertService) => _alertService = alertService;

    // consultar una alerta
    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _alertService.GetByIdAsync(id, ct);

        if (result.IsFailure)
            return NotFound(result.Error);

        var response = new AlertResponse(
            result.Value.Id, result.Value.Type, result.Value.Description,
            result.Value.ResolvedAt, result.Value.IsResolved,
            result.Value.ResourceId, result.Value.SpaceId);

        return Ok(response);
    }

    // reportar una nueva incidencia (daño, mantenimiento, etc.)
    [HttpPost]
    public async Task<IActionResult> Create(CreateAlertRequest request, CancellationToken ct)
    {
        var result = await _alertService.CreateAsync(
            request.Type, request.Description, request.ResourceId, request.SpaceId, ct);

        if (result.IsFailure)
            return BadRequest(result.Error); 

        var response = new AlertResponse(
            result.Value.Id, result.Value.Type, result.Value.Description,
            result.Value.ResolvedAt, result.Value.IsResolved,
            result.Value.ResourceId, result.Value.SpaceId);

        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, response);
    }

    // marcar la incidencia como resuelta
    [HttpPatch("{id:guid}/resolve")]
    public async Task<IActionResult> Resolve(Guid id, CancellationToken ct)
    {
        var result = await _alertService.ResolveAsync(id, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }
}