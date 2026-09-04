using SpaceReservationSystem.Domain.Entities;
using SpaceReservationSystem.Domain.Enums;
using SpaceReservationSystem.Domain.Errors;
using SpaceReservationSystem.Domain.Interfaces;
using SpaceReservationSystem.Domain.Primitives;
using SpaceReservationSystem.Domain.ValueObjects;
using SpaceReservationSystem.Infrastructure.Authentication;
// Alias para evitar conflicto con el namespace Application.Features.Email
using EmailValueObject = SpaceReservationSystem.Domain.ValueObjects.Email;

namespace SpaceReservationSystem.Application.Features.Auth;

public class AuthService(
    IUserRepository userRepository,
    IRoleRepository roleRepository,
    IUnitOfWork unitOfWork,
    IPasswordHasher passwordHasher,
    ITokenService tokenService
)
{
    public async Task<Result<RegisterResponse>> RegisterAsync(RegisterRequest request, CancellationToken ct)
    {
        if(string.IsNullOrWhiteSpace(request.Password) || request.Password.Length < 6)
            return Result.Failure<RegisterResponse>(Error.Validation("Password", "Password must be at least 6 characters."));

        //var emailResult = Email.Create(request.Email);
        // Usamos el Email del Domain, no el módulo Application.Features.Email.
        var emailResult = EmailValueObject.Create(request.Email);
        if (emailResult.IsFailure)
            return Result.Failure<RegisterResponse>(emailResult.Error);

        if(await userRepository.ExistsByEmailAsync(emailResult.Value, ct))
            return Result.Failure<RegisterResponse>(UserErrors.InvalidEmail);

        var role = await roleRepository.GetByCodeAsync(RoleCode.Student, ct);
        if(role is null)
            return Result.Failure<RegisterResponse>(RoleErrors.NotFound);

        var passwordHash = passwordHasher.Hash(request.Password);

        var userResult = User.Create(request.Name, emailResult.Value, passwordHash, request.Phone, role.Id);
        if(userResult.IsFailure)
            return Result.Failure<RegisterResponse>(userResult.Error);

        var user = userResult.Value;
        userRepository.Add(user);
        await unitOfWork.SaveChangesAsync(ct);

        //!important: Asignamos el Role en memoria para poder generar el claim sin otro round-trip a la DB
        // typeof(User).GetProperty(nameof(User.Role))!.SetValue(user, role);

        var accessToken = tokenService.GenerateAccessToken(user, role);
        var refreshToken = tokenService.GenerateRefreshToken();

        return new RegisterResponse(user.Id, accessToken, refreshToken);
    }

    public async Task<Result<LoginResponse>> LoginAsync(LoginRequest request, CancellationToken ct)
    {
        //var emailResult = Email.Create(request.Email);
        // Usamos el Email del Domain, no el módulo Application.Features.Email.
        var emailResult = EmailValueObject.Create(request.Email);
        if (emailResult.IsFailure)
            return Result.Failure<LoginResponse>(UserErrors.InvalidEmail);

        var user =  await userRepository.GetByEmailAsync(emailResult.Value, ct); // ya incluye Role (Include agregado en UserRepository)
        if (user is null || !passwordHasher.Verify(request.Password, user.PasswordHash))
            return Result.Failure<LoginResponse>(Error.Validation("Credentials", "Invalid email or password."));

        var role = await roleRepository.GetByIdAsync(user.RoleId, ct);
        if (role is null)
            return Result.Failure<LoginResponse>(RoleErrors.NotFound);

        var accessToken = tokenService.GenerateAccessToken(user, role);
        var refreshToken = tokenService.GenerateRefreshToken();

        return new LoginResponse(user.Id, accessToken, refreshToken);
    }
}