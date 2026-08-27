using Microsoft.EntityFrameworkCore;
using SpaceReservationSystem.Domain.Entities;
using SpaceReservationSystem.Domain.Enums;
using SpaceReservationSystem.Domain.Interfaces;
using SpaceReservationSystem.Infrastructure.Data;

namespace SpaceReservationSystem.Infrastructure.Persistence.Repositories;

public class RoleRepository : IRoleRepository
{
    private readonly AppDbContext _context;

    public RoleRepository(AppDbContext context) => _context = context;

    public async Task<Role?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.Roles.FirstOrDefaultAsync(r => r.Id == id, ct);

    public void Add(Role role) => _context.Roles.Add(role);

    public void Update(Role role) => _context.Roles.Update(role);

    public async Task<Role?> GetByCodeAsync(RoleCode code, CancellationToken ct = default)
        => await _context.Roles.FirstOrDefaultAsync(r => r.Code == code, ct);
}