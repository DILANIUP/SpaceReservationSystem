using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Infrastructure.Authentication;

public interface ITokenService
{
    string GenerateAccessToken(User user, Role role);
    string GenerateRefreshToken();
}