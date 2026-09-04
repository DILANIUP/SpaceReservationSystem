using EmailTemplateEntity = SpaceReservationSystem.Domain.Entities.EmailTemplate;
using SpaceReservationSystem.Domain.Interfaces;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Application.Features.EmailTemplate;

public class EmailTemplateService
{
    private readonly IEmailTemplateRepository _templateRepository;
    private readonly IUnitOfWork _unitOfWork;

    public EmailTemplateService(
        IEmailTemplateRepository templateRepository,
        IUnitOfWork unitOfWork)
    {
        _templateRepository = templateRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<EmailTemplateEntity>> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var template = await _templateRepository.GetByIdAsync(id, ct);

        if (template is null)
            return Result.Failure<EmailTemplateEntity>(
                Error.NotFound("EmailTemplate", id.ToString()));

        return template;
    }

    public async Task<Result<EmailTemplateEntity>> CreateAsync(
        string code,
        string subject,
        string body,
        CancellationToken ct = default)
    {
        var result = EmailTemplateEntity.Create(code, subject, body);

        if (result.IsFailure)
            return Result.Failure<EmailTemplateEntity>(result.Error);

        _templateRepository.Add(result.Value);

        await _unitOfWork.SaveChangesAsync(ct);

        return result.Value;
    }

    public async Task<Result> UpdateAsync(
        Guid id,
        string subject,
        string body,
        CancellationToken ct = default)
    {
        var template = await _templateRepository.GetByIdAsync(id, ct);

        if (template is null)
            return Result.Failure(
                Error.NotFound("EmailTemplate", id.ToString()));

        var result = template.Update(subject, body);

        if (result.IsFailure)
            return result;

        _templateRepository.Update(template);

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}