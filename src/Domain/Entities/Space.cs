using SpaceReservationSystem.Domain.Enums;
using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.Entities;

public class Space : AuditableEntity
{
    public string Name { get; private set; } = null!;
    public SpaceType Type { get; private set; }
    public int Capacity { get; private set; }
    public string Location { get; private set; } = null!;
    public bool IsActive { get; private set; }

    public ICollection<Reservation> Reservations { get; private set; } = new List<Reservation>();
    public ICollection<Alert> Alerts { get; private set; } = new List<Alert>();

    private Space(Guid id, string name, SpaceType type, int capacity, string location)
        : base(id)
    {
        Name = name;
        Type = type;
        Capacity = capacity;
        Location = location;
        IsActive = true;
    }

    private Space() { }

    public static Result<Space> Create(string name, SpaceType type, int capacity, string location)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Space>(SpaceErrors.InvalidName);

        if (capacity <= 0)
            return Result.Failure<Space>(SpaceErrors.InvalidCapacity);

        if (string.IsNullOrWhiteSpace(location))
            return Result.Failure<Space>(SpaceErrors.InvalidLocation);

        return new Space(Guid.NewGuid(), name.Trim(), type, capacity, location.Trim());
    }

    public Result Update(string name, int capacity, string location)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(SpaceErrors.InvalidName);

        if (capacity <= 0)
            return Result.Failure(SpaceErrors.InvalidCapacity);

        if (string.IsNullOrWhiteSpace(location))
            return Result.Failure(SpaceErrors.InvalidLocation);

        Name = name.Trim();
        Capacity = capacity;
        Location = location.Trim();
        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!IsActive)
            return Result.Failure(SpaceErrors.AlreadyInactive);

        IsActive = false;
        return Result.Success();
    }

    public Result Activate()
    {
        if (IsActive)
            return Result.Failure(SpaceErrors.AlreadyActive);

        IsActive = true;
        return Result.Success();
    }
}