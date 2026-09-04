using EmailLogEntity = SpaceReservationSystem.Domain.Entities.EmailLog;
using SpaceReservationSystem.API.Abstractions.Email;
using SpaceReservationSystem.Domain.Interfaces;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Application.Features.Email;

// Orquestador: busca la plantilla, envía el correo real vía SMTP, y deja registro en EmailLog.
public class EmailService
{
    private readonly IEmailTemplateRepository _templateRepository;
    private readonly IEmailLogRepository _logRepository;
    private readonly IEmailService _emailSender; // implementación real (SMTP)
    private readonly IUnitOfWork _unitOfWork;

    public EmailService(
        IEmailTemplateRepository templateRepository,
        IEmailLogRepository logRepository,
        IEmailService emailSender,
        IUnitOfWork unitOfWork)
    {
        _templateRepository = templateRepository;
        _logRepository = logRepository;
        _emailSender = emailSender;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<EmailLogEntity>> SendByTemplateAsync(
        string templateCode,
        string toEmail,
        Guid? reservationId = null,
        Guid? alertId = null,
        CancellationToken ct = default)
    {
        var template = await _templateRepository.GetByCodeAsync(templateCode, ct);

        if (template is null)
            return Result.Failure<EmailLogEntity>(
                Error.NotFound("EmailTemplate", templateCode));

        var logResult = EmailLogEntity.Create(toEmail, template.Id, reservationId, alertId);

        if (logResult.IsFailure)
            return Result.Failure<EmailLogEntity>(logResult.Error);

        var log = logResult.Value;

        try
        {
            await _emailSender.SendEmailAsync(toEmail, template.Subject, template.Body, ct);
            log.MarkAsSent();
        }
        catch (Exception ex)
        {
            log.MarkAsFailed(ex.Message);
        }

        _logRepository.Add(log);
        await _unitOfWork.SaveChangesAsync(ct);

        return log;
    }
}