using Microsoft.AspNetCore.Mvc;
using SpaceReservationSystem.Application.Features.Space;

namespace SpaceReservationSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class SpaceController : ControllerBase
{
    private readonly SpaceService _spaceService;

    public SpaceController(SpaceService spaceService)
    {
        _spaceService = spaceService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken ct)
    {
        var result = await _spaceService.GetByIdAsync(id, ct);

        if (result.IsFailure)
            return NotFound(result.Error);

        var response = new SpaceResponse(
            result.Value.Id,
            result.Value.Name,
            result.Value.Type,
            result.Value.Capacity,
            result.Value.Location,
            result.Value.IsActive);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateSpaceRequest request,
        CancellationToken ct)
    {
        var result = await _spaceService.CreateAsync(
            request.Name,
            request.Type,
            request.Capacity,
            request.Location,
            ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        var response = new SpaceResponse(
            result.Value.Id,
            result.Value.Name,
            result.Value.Type,
            result.Value.Capacity,
            result.Value.Location,
            result.Value.IsActive);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value.Id },
            response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateSpaceRequest request,
        CancellationToken ct)
    {
        var result = await _spaceService.UpdateAsync(
            id,
            request.Name,
            request.Capacity,
            request.Location,
            ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpPatch("{id:guid}/activate")]
    public async Task<IActionResult> Activate(
        Guid id,
        CancellationToken ct)
    {
        var result = await _spaceService.ActivateAsync(id, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(
        Guid id,
        CancellationToken ct)
    {
        var result = await _spaceService.DeactivateAsync(id, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }
}