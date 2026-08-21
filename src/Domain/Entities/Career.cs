using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.Entities;

public class Career : AuditableEntity
{
    public string Name { get; private set; } = null!;
    public Guid FacultyId { get; set; }
    public Faculty Faculty { get; set; } = null!;
    public ICollection<User> Users { get; set; } = new List<User>();



    private Career(Guid id, string name, Guid facultyId) : base(id)
    {
        Name = name;
        FacultyId = facultyId;
    }

    private Career () { }

    public static Result<Career> Create (string name, Guid facultyId)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure<Career>(CareerErrors.InvalidName);

        if (facultyId == Guid.Empty)
            return Result.Failure<Career>(CareerErrors.InvalidFaculty);

        return new Career(Guid.NewGuid(), name.Trim(), facultyId);
    }

    public Result Update (string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            return Result.Failure(CareerErrors.InvalidName);
    
        Name = name.Trim();
        return Result.Success();
    }

    

}  