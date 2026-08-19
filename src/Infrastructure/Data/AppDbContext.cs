using Microsoft.EntityFrameworkCore;

namespace SpaceReservationSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) 
    : base(options) { }

}