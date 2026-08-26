using System.Text;
using Microsoft.EntityFrameworkCore;
using SpaceReservationSystem.Domain.Interfaces;
using SpaceReservationSystem.Infrastructure.Data;
using SpaceReservationSystem.Infrastructure.Persistence;
using SpaceReservationSystem.Infrastructure.Persistence.Repositories;

namespace SpaceReservationSystem.Infrastructure;

public static class DependencyInjection
{
    public static IServiceCollection AddInfrastructure(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDatabase(configuration);
        services.AddRepositories();
        return services;
    }

    private static void AddDatabase(this IServiceCollection services, IConfiguration configuration)
    {
        services.AddDbContext<AppDbContext>(options =>
            options.UseNpgsql(configuration.GetConnectionString("Default")));

        services.AddScoped<IUnitOfWork, UnitOfWork>();
    }

    private static void AddRepositories(this IServiceCollection services)
    {
        services.AddScoped<IRoleRepository, RoleRepository>();
        services.AddScoped<IFacultyRepository, FacultyRepository>();
        services.AddScoped<ICareerRepository, CareerRepository>();
        services.AddScoped<IUserRepository, UserRepository>();
        services.AddScoped<ISpaceRepository, SpaceRepository>();
        services.AddScoped<IResourceRepository, ResourceRepository>();
        services.AddScoped<IReservationRepository, ReservationRepository>();
        services.AddScoped<IVoucherRepository, VoucherRepository>();
        services.AddScoped<IAlertRepository, AlertRepository>();
        services.AddScoped<IEmailTemplateRepository, EmailTemplateRepository>();
        services.AddScoped<IEmailLogRepository, EmailLogRepository>();
    }

    
}