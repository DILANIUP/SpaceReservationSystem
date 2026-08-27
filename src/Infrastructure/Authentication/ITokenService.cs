using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Infrastructure.Authentication;

public interface ITokenService
{
    string GenerateAccessToken(User user);
    string GenerateRefreshToken();
}