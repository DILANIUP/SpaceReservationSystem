using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.Errors;

public static class RoleErrors
{
    public static readonly Error InvalidName = new  ("Role.InvalidName", "the name of the role is invalid");
}