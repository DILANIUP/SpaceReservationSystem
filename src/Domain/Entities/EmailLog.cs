using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.Entities;

public class EmailLog : AuditableEntity
{
    public string ToEmail { get; private set; } = null!;
    public DateTime SentAt { get; private set; }
    public bool IsSent { get; private set; }
    public string? ErrorMessage { get; private set; }

    public Guid? ReservationId { get; private set; }
    public Reservation? Reservation { get; private set; }

    public Guid? AlertId { get; private set; }
    public Alert? Alert { get; private set; }

    public Guid TemplateId { get; private set; }
    public EmailTemplate Template { get; private set; } = null!;

    private EmailLog(Guid id, string toEmail, Guid templateId, Guid? reservationId, Guid? alertId)
        : base(id)
    {
        ToEmail = toEmail;
        TemplateId = templateId;
        ReservationId = reservationId;
        AlertId = alertId;
        SentAt = DateTime.UtcNow;
        IsSent = false;
    }

    private EmailLog() { }

    public static Result<EmailLog> Create(
        string toEmail,
        Guid templateId,
        Guid? reservationId = null,
        Guid? alertId = null)
    {
        if (string.IsNullOrWhiteSpace(toEmail))
            return Result.Failure<EmailLog>(EmailLogErrors.InvalidEmail);

        if (templateId == Guid.Empty)
            return Result.Failure<EmailLog>(EmailLogErrors.InvalidTemplate);

        return new EmailLog(Guid.NewGuid(), toEmail.Trim(), templateId, reservationId, alertId);
    }

    public Result MarkAsSent()
    {
        IsSent = true;
        SentAt = DateTime.UtcNow;
        ErrorMessage = null;
        return Result.Success();
    }

    public Result MarkAsFailed(string errorMessage)
    {
        if (string.IsNullOrWhiteSpace(errorMessage))
            return Result.Failure(EmailLogErrors.InvalidErrorMessage);

        IsSent = false;
        ErrorMessage = errorMessage;
        return Result.Success();
    }

}