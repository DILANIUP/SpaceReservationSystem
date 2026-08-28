using FacultyEntity = SpaceReservationSystem.Domain.Entities.Faculty;
using SpaceReservationSystem.Domain.Interfaces;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Application.Features.Faculty;

public class FacultyService
{
    private readonly IFacultyRepository _facultyRepository;
    private readonly IUnitOfWork _unitOfWork;

    public FacultyService(
        IFacultyRepository facultyRepository,
        IUnitOfWork unitOfWork)
    {
        _facultyRepository = facultyRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<Result<FacultyEntity>> GetByIdAsync(
        Guid id,
        CancellationToken ct = default)
    {
        var faculty = await _facultyRepository.GetByIdAsync(id, ct);

        if (faculty is null)
            return Result.Failure<FacultyEntity>(
                Error.NotFound("Faculty", id.ToString()));

        return faculty;
    }

    public async Task<Result<FacultyEntity>> CreateAsync(
        string name,
        CancellationToken ct = default)
    {
        var result = FacultyEntity.Create(name);

        if (result.IsFailure)
            return Result.Failure<FacultyEntity>(result.Error);

        _facultyRepository.Add(result.Value);

        await _unitOfWork.SaveChangesAsync(ct);

        return result.Value;
    }

    public async Task<Result> UpdateAsync(
        Guid id,
        string name,
        CancellationToken ct = default)
    {
        var faculty = await _facultyRepository.GetByIdAsync(id, ct);

        if (faculty is null)
            return Result.Failure(
                Error.NotFound("Faculty", id.ToString()));

        var result = faculty.Update(name);

        if (result.IsFailure)
            return result;

        _facultyRepository.Update(faculty);

        await _unitOfWork.SaveChangesAsync(ct);

        return Result.Success();
    }
}