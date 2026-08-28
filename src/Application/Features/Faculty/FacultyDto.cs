namespace SpaceReservationSystem.Application.Features.Faculty;

public record CreateFacultyRequest(
    string Name
);

public record UpdateFacultyRequest(
    string Name
);

public record FacultyResponse(
    Guid Id,
    string Name
);