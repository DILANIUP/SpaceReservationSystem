namespace SpaceReservationSystem.Application.Features.Career;

public record CreateCareerRequest(
    string Name,
    Guid FacultyId
);

public record UpdateCareerRequest(
    string Name
);

public record CareerResponse(
    Guid Id,
    string Name,
    Guid FacultyId
);