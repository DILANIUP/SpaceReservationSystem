

using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.Entities;

public class Faculty : AuditableEntity
{
    public string Name { get; set; } = null!;
    public ICollection<Career> Careers { get; set; } = new List<Career>();

    private Faculty(Guid id, string name) : base(id)
    {
        Name = name;
    }

    private Faculty (){ }

    public static Result<Faculty> Create(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Faculty>(FacultyErrors.InvalidName);

        return new Faculty(Guid.NewGuid(), name.Trim());
    }

    public Result Update(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(FacultyErrors.InvalidName);

        Name = name.Trim();
        return Result.Success();
    }
    
}