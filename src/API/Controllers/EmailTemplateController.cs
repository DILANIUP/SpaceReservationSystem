using Microsoft.AspNetCore.Mvc;
using SpaceReservationSystem.Application.Features.EmailTemplate;

namespace SpaceReservationSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailTemplateController : ControllerBase
{
    private readonly EmailTemplateService _templateService;

    public EmailTemplateController(EmailTemplateService templateService)
    {
        _templateService = templateService;
    }

    [HttpGet("{id:guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken ct)
    {
        var result = await _templateService.GetByIdAsync(id, ct);

        if (result.IsFailure)
            return NotFound(result.Error);

        var response = new EmailTemplateResponse(
            result.Value.Id, result.Value.Code, result.Value.Subject, result.Value.Body);

        return Ok(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create(CreateEmailTemplateRequest request, CancellationToken ct)
    {
        var result = await _templateService.CreateAsync(
            request.Code, request.Subject, request.Body, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        var response = new EmailTemplateResponse(
            result.Value.Id, result.Value.Code, result.Value.Subject, result.Value.Body);

        return CreatedAtAction(nameof(GetById), new { id = result.Value.Id }, response);
    }

    [HttpPut("{id:guid}")]
    public async Task<IActionResult> Update(Guid id, UpdateEmailTemplateRequest request, CancellationToken ct)
    {
        var result = await _templateService.UpdateAsync(id, request.Subject, request.Body, ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return NoContent();
    }
}