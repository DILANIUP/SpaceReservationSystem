using SpaceReservationSystem.Domain.Enums;

namespace SpaceReservationSystem.Application.Features.Alert;

// Datos que llegan al crear una alerta
public record CreateAlertRequest(
    AlertType Type,
    string Description,
    Guid? ResourceId,   
    Guid? SpaceId       
);

// Datos que se devuelven una alerta
public record AlertResponse(
    Guid Id,
    AlertType Type,
    string Description,
    DateTime? ResolvedAt,  // null mientras no se resuelva
    bool IsResolved,
    Guid? ResourceId,
    Guid? SpaceId
);