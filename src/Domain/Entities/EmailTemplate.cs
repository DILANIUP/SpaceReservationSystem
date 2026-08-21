using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.Entities;

public class EmailTemplate : AuditableEntity
{
    public string Code { get; private set; } = null!;
    public string Subject { get; private set; } = null!;
    public string Body { get; private set; } = null!;

    public ICollection<EmailLog> EmailLogs { get; set; } = new List<EmailLog>();


    private EmailTemplate(Guid id, string code, string subject, string body) : base(id)
    {
        Code = code;
        Subject = subject;
        Body = body;
    }

    private EmailTemplate() { }

     public static Result<EmailTemplate> Create(string code, string subject, string body)
    {
        if (string.IsNullOrWhiteSpace(code))
            return Result.Failure<EmailTemplate>(EmailTemplateErrors.InvalidCode);

        if (string.IsNullOrWhiteSpace(subject))
            return Result.Failure<EmailTemplate>(EmailTemplateErrors.InvalidSubject);

        if (string.IsNullOrWhiteSpace(body))
            return Result.Failure<EmailTemplate>(EmailTemplateErrors.InvalidBody);

        return new EmailTemplate(Guid.NewGuid(), code.Trim(), subject.Trim(), body);
    }

    public Result Update(string subject, string body)
    {
        if (string.IsNullOrWhiteSpace(subject))
            return Result.Failure(EmailTemplateErrors.InvalidSubject);

        if (string.IsNullOrWhiteSpace(body))
            return Result.Failure(EmailTemplateErrors.InvalidBody);

        Subject = subject.Trim();
        Body = body;
        return Result.Success();
    }
}