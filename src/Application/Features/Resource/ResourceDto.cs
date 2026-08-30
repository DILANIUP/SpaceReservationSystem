namespace SpaceReservationSystem.Application.Features.Resource;

public record CreateResourceRequest(
    string Name,
    string? Description,
    int AvailableQuantity
);

public record UpdateResourceRequest(
    string Name,
    string? Description,
    int AvailableQuantity
);

public record ResourceResponse(
    Guid Id,
    string Name,
    string? Description,
    int AvailableQuantity,
    bool Status
);