using SpaceReservationSystem.Domain.Enums;

namespace SpaceReservationSystem.Application.Features.Space;

public record CreateSpaceRequest (
    string Name,
    SpaceType Type,
    int Capacity,
    string Location
);

public record UpdateSpaceRequest
(
    string Name,
    int Capacity,
    string Location
);
public record SpaceResponse (
    Guid Id,
     string Name,
     SpaceType Type,
     int Capacity,
     string Location,
     bool IsActive
);