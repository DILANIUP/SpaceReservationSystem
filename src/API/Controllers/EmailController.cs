using Microsoft.AspNetCore.Mvc;
using SpaceReservationSystem.Application.Features.Email;

namespace SpaceReservationSystem.API.Controllers;

[ApiController]
[Route("api/[controller]")]
public class EmailController : ControllerBase
{
    private readonly EmailService _emailService;

    public EmailController(EmailService emailService)
    {
        _emailService = emailService;
    }

    [HttpPost("send")]
    public async Task<IActionResult> Send(SendEmailRequest request, CancellationToken ct)
    {
        var result = await _emailService.SendByTemplateAsync(
            request.TemplateCode,
            request.ToEmail,
            request.ReservationId,
            request.AlertId,
            ct);

        if (result.IsFailure)
            return BadRequest(result.Error);

        return Ok(new
        {
            result.Value.Id,
            result.Value.IsSent,
            result.Value.ErrorMessage
        });
    }
}