using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Primitives;

namespace SpaceReservationSystem.Domain.ValueObjects;

public sealed class Email : IEquatable<Email>
{
    public string Value { get; }

    private Email(string value) => Value = value;

    public static Result<Email> Create(string? email)
    {
        if (string.IsNullOrWhiteSpace(email))
            return Result.Failure<Email>(UserErrors.InvalidEmail);

        if (!email.Contains('@') || !email.Contains('.'))
            return Result.Failure<Email>(UserErrors.InvalidEmail);

        return new Email(email.Trim().ToLowerInvariant());
    }

    public bool Equals(Email? other) => other is not null && Value == other.Value;
    public override bool Equals(object? obj) => Equals(obj as Email);
    public override int GetHashCode() => Value.GetHashCode();
    public override string ToString() => Value;
}