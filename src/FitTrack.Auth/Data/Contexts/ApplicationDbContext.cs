using FitTrack.Auth.Data.DbModels;
using Microsoft.EntityFrameworkCore;

namespace FitTrack.Auth.Data.Contexts;

public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<ApplicationUser> Users { get; set; }
    public DbSet<ApplicationRole> Roles { get; set; }
    public DbSet<ApplicationUserRole> UserRoles { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<ApplicationUserRole>()
            .HasKey(userRole => new { userRole.UserId, userRole.RoleId });

        modelBuilder.Entity<ApplicationUserRole>()
            .HasOne(userRole => userRole.User)
            .WithMany(user => user.UserRoles)
            .HasForeignKey(user => user.UserId);

        modelBuilder.Entity<ApplicationUserRole>()
            .HasOne(user => user.Role)
            .WithMany(role => role.UserRoles)
            .HasForeignKey(user => user.RoleId);
    }
}
