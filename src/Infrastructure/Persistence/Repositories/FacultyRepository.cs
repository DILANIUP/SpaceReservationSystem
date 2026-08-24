using Microsoft.EntityFrameworkCore;
using SpaceReservationSystem.Domain.Entities;
using SpaceReservationSystem.Domain.Interfaces;
using SpaceReservationSystem.Infrastructure.Data;

namespace SpaceReservationSystem.Infrastructure.Persistence.Repositories;

public class FacultyRepository : IFacultyRepository
{
    private readonly AppDbContext _context;

    public FacultyRepository(AppDbContext context) => _context = context;

    public async Task<Faculty?> GetByIdAsync(Guid id, CancellationToken ct = default)
        => await _context.Faculties.FirstOrDefaultAsync(f => f.Id == id, ct);

    public void Add(Faculty faculty) => _context.Faculties.Add(faculty);

    public void Update(Faculty faculty) => _context.Faculties.Update(faculty);
}