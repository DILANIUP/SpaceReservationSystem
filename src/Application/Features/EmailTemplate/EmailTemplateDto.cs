namespace SpaceReservationSystem.Application.Features.EmailTemplate;

// Datos para crear una plantilla nueva (ej: Code="ReservationApproved")
public record CreateEmailTemplateRequest(
    string Code,
    string Subject,
    string Body
);

// Datos para editar el asunto/cuerpo de una plantilla existente (el Code no se edita)
public record UpdateEmailTemplateRequest(
    string Subject,
    string Body
);

public record EmailTemplateResponse(
    Guid Id,
    string Code,
    string Subject,
    string Body
);