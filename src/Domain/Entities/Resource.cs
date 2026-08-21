using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.Entities;

public class Resource : AuditableEntity
{
    public string Name { get; private set; } = null!;
    public string? Description { get; private set; }
    public int AvailableQuantity { get; private set; }
    public bool Status { get; private set; }

    public ICollection<ReservationResource> ReservationResources { get; private set; } = new List<ReservationResource>();
    public ICollection<Alert> Alerts { get; private set; } = new List<Alert>();

    private Resource(Guid id, string name, string? description, int availableQuantity, bool status)
    {
        Name = name;
        Description = description;
        AvailableQuantity = availableQuantity;
        Status = status;
    }

    private Resource() { }

    public static Result<Resource> Create(
        string name,
        string? description,
        int availableQuantity,
        bool status = true
    )
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Resource>(ResourceErrors.InvalidName);

        if (availableQuantity < 0)
            return Result.Failure<Resource>(ResourceErrors.InvalidQuantity);

        return new Resource(Guid.NewGuid(), name.Trim(), description?.Trim(), availableQuantity, status);
    }

    public Result Update(string name, string? description, int availableQuantity)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(ResourceErrors.InvalidName);

        if (availableQuantity < 0)
            return Result.Failure(ResourceErrors.InvalidQuantity);

        Name = name.Trim();
        Description = description?.Trim();
        AvailableQuantity = availableQuantity;
        return Result.Success();
    }

    public Result Activate()
    {
        if (Status)
            return Result.Failure(ResourceErrors.AlreadyActive);

        Status = true;
        return Result.Success();
    }

    public Result Deactivate()
    {
        if (!Status)
            return Result.Failure(ResourceErrors.AlreadyInactive);

        Status = false;
        return Result.Success();
    }
}