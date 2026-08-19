using Microsoft.EntityFrameworkCore;
using SpaceReservationSystem.Domain.Entities;

namespace SpaceReservationSystem.Infrastructure.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options)
    : base(options) { }

    public DbSet<Role> Roles => Set<Role>();
    public DbSet<Faculty> Faculties => Set<Faculty>();
    public DbSet<Career> Careers => Set<Career>();
    public DbSet<User> Users => Set<User>();
    public DbSet<Space> Spaces => Set<Space>();
    public DbSet<Resource> Resources => Set<Resource>();
    public DbSet<Reservation> Reservations => Set<Reservation>();
    public DbSet<ReservationResource> ReservationResources => Set<ReservationResource>();
    public DbSet<ReservationHistory> ReservationHistories => Set<ReservationHistory>();
    public DbSet<Voucher> Vouchers => Set<Voucher>();
    public DbSet<Alert> Alerts => Set<Alert>();
    public DbSet<EmailTemplate> EmailTemplates => Set<EmailTemplate>();
    public DbSet<EmailLog> EmailLogs => Set<EmailLog>();
}