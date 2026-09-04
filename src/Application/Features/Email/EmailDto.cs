namespace SpaceReservationSystem.Application.Features.Email;

// Petición para enviar un correo usando una plantilla por su Code (ej: "ReservationApproved")
public record SendEmailRequest(
    string TemplateCode,
    string ToEmail,
    Guid? ReservationId,
    Guid? AlertId
);