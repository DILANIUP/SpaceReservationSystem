using SpaceReservationSystem.Domain.Enums;
using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.Entities;

public class Role : AuditableEntity
{
    public string Name { get; private set; } = null!;
    public  RoleCode Code { get; private set; }
    public  ICollection<User> Users { get; private set; } = new List<User>();

    private Role(Guid id, string name, RoleCode code) : base(id)
    {
        Name = name;
        Code = code;
    }

    private Role(){}

    public static Result<Role> Create(string name, RoleCode code)
    {
        if(string.IsNullOrWhiteSpace(name))
            return Result.Failure<Role>(RoleErrors.InvalidName);

        var role = new Role(Guid.NewGuid(), name.Trim(), code);
            return role;
    }

    public Result Update(string name)
    {
        if(string.IsNullOrWhiteSpace(name))
            return Result.Failure(RoleErrors.InvalidName);

        Name = name.Trim();
        return Result.Success();
    }
}