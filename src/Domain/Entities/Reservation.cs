using SpaceReservationSystem.Domain.Enums;
using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.Entities;

public class Reservation : AuditableEntity
{
    private static readonly TimeSpan MinAllowedTime = new(7, 0, 0);
    private static readonly TimeSpan MaxAllowedTime = new(23, 0, 0);
    private const int MinNoticeHours = 72;
    public DateTime Date { get; private set; }
    public TimeSpan StartTime { get; private set; }
    public TimeSpan EndTime { get; private set; }
    public string Reason { get; private set; } = null!;
    public ReservationStatus CurrentStatus { get; private set; }
    public DateTime RequestDate { get; private set; }

    public Guid UserId { get; private set; }
    public User User { get; private set; } = null!;

    public Guid? SpaceId { get; private set; }
    public Space? Space { get; private set; }
    // Navegacion Inversa
    public Voucher? Voucher { get; private set; }

    public ICollection<ReservationResource> ReservationResources { get; private set; } = new List<ReservationResource>();
    public ICollection<ReservationHistory> ReservationHistories { get; private set; } = new List<ReservationHistory>();


    private Reservation(
        Guid id,
        DateTime date,
        TimeSpan startTime,
        TimeSpan endTime,
        string reason,
        Guid userId,
        Guid? spaceId
    ) : base(id)
    {
        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Reason = reason;
        CurrentStatus = ReservationStatus.Draft;
        RequestDate = DateTime.UtcNow;
        UserId = userId;
        SpaceId = spaceId;
    }
    
    private Reservation() { }

    public static Result<Reservation> Create(
        DateTime date,
        TimeSpan startTime,
        TimeSpan endTime,
        string reason,
        Guid userId,
        Guid? spaceId
    )
    {
        if (string.IsNullOrWhiteSpace(reason))
            return Result.Failure<Reservation>(ReservationErrors.InvalidReason);

        if (userId == Guid.Empty)
            return Result.Failure<Reservation>(ReservationErrors.InvalidUser);

        if (endTime <= startTime)
            return Result.Failure<Reservation>(ReservationErrors.InvalidTimeRange);

        if (startTime < MinAllowedTime || endTime > MaxAllowedTime)
            return Result.Failure<Reservation>(ReservationErrors.OutsideAllowedHours);

        var requestDate = DateTime.UtcNow;
        if (date < requestDate.AddHours(MinNoticeHours))
            return Result.Failure<Reservation>(ReservationErrors.InsufficientNotice);

        return new Reservation(Guid.NewGuid(), date, startTime, endTime, reason.Trim(), userId, spaceId);
    }

    public Result SubmitToCoordinator()
    {
        if (CurrentStatus != ReservationStatus.Draft)
            return Result.Failure(ReservationErrors.InvalidStatusTransition);

        CurrentStatus = ReservationStatus.PendingCoordinator;
        return Result.Success();
    }

    public Result ElevatedToVicerrector()
    {
        if (CurrentStatus != ReservationStatus.PendingCoordinator)
            return Result.Failure(ReservationErrors.InvalidStatusTransition);

        CurrentStatus = ReservationStatus.PendingVicerrector;
        return Result.Success();
    }

    public Result ApproveByVicerrector()
    {
        if (CurrentStatus != ReservationStatus.PendingVicerrector)
            return Result.Failure(ReservationErrors.InvalidStatusTransition);

        CurrentStatus = ReservationStatus.PendingAssets;
        return Result.Success();
    }

    public Result AssignBySpaceManagement()
    {
        if (CurrentStatus != ReservationStatus.PendingAssets)
            return Result.Failure(ReservationErrors.InvalidStatusTransition);

        CurrentStatus = ReservationStatus.Approved;
        return Result.Success();
    }

    public Result Reject()
    {
        if (CurrentStatus is ReservationStatus.Approved or ReservationStatus.Rejected)
            return Result.Failure(ReservationErrors.InvalidStatusTransition);

        CurrentStatus = ReservationStatus.Rejected;
        return Result.Success();
    }

    public Result Cancel()
    {
        if (CurrentStatus is ReservationStatus.Rejected or ReservationStatus.Cancelled)
            return Result.Failure(ReservationErrors.InvalidStatusTransition);
    
        CurrentStatus = ReservationStatus.Cancelled;
        return Result.Success();    
    }

    public Result Update(DateTime date, TimeSpan startTime, TimeSpan endTime, string reason)
{
        if (CurrentStatus != ReservationStatus.Draft)
            return Result.Failure(ReservationErrors.InvalidStatusTransition);

        if (string.IsNullOrWhiteSpace(reason))
            return Result.Failure(ReservationErrors.InvalidReason);

        if (endTime <= startTime)
            return Result.Failure(ReservationErrors.InvalidTimeRange);

        if (startTime < MinAllowedTime || endTime > MaxAllowedTime)
            return Result.Failure(ReservationErrors.OutsideAllowedHours);

        var requestDate = DateTime.UtcNow;
        if (date < requestDate.AddHours(MinNoticeHours))
            return Result.Failure(ReservationErrors.InsufficientNotice);

        Date = date;
        StartTime = startTime;
        EndTime = endTime;
        Reason = reason.Trim();
        return Result.Success();
}

}