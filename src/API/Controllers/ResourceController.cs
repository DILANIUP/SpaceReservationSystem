using Microsoft.AspNetCore.Mvc;
using SpaceReservationSystem.Application.Features.Resource;

namespace SpaceReservationSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ResourceController : ControllerBase
{
    private readonly ResourceService _resourceService;

    public ResourceController(ResourceService resourceService)
    {
        _resourceService = resourceService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(
        Guid id,
        CancellationToken ct)
    {
        var result = await _resourceService.GetByIdAsync(id, ct);

        if (result.IsFailure)
            return NotFound(result.Error);

        var response = new ResourceResponse(
            result.Value.Id,
            result.Value.Name,
            result.Value.Description,
            result.Value.AvailableQuantity,
            result.Value.Status);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(
        CreateResourceRequest request,
        CancellationToken ct)
    {
        var result = await _resourceService.CreateAsync(
            request.Name,
            request.Description,
            request.AvailableQuantity,
            ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        var response = new ResourceResponse(
            result.Value.Id,
            result.Value.Name,
            result.Value.Description,
            result.Value.AvailableQuantity,
            result.Value.Status);

        return CreatedAtAction(
            nameof(GetById),
            new { id = result.Value.Id },
            response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(
        Guid id,
        UpdateResourceRequest request,
        CancellationToken ct)
    {
        var result = await _resourceService.UpdateAsync(
            id,
            request.Name,
            request.Description,
            request.AvailableQuantity,
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
        var result = await _resourceService.ActivateAsync(id, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }

    [HttpPatch("{id:guid}/deactivate")]
    public async Task<IActionResult> Deactivate(
        Guid id,
        CancellationToken ct)
    {
        var result = await _resourceService.DeactivateAsync(id, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }
}