using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ESLBackend.models;
using ESLBackend.Models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<User> Users { get; set; }
    public DbSet<Organization> Organizations { get; set; }

    public DbSet<MeetingRoom> MeetingRooms { get; set; }


    public DbSet<Templates> Templates { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the primary key for User
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);

        // Configure the optional relationship with Organization
        modelBuilder.Entity<User>()
            .HasOne(u => u.Organization)
            .WithMany() // Assuming an organization can have multiple users
            .HasForeignKey(u => u.OrganizationId) // Use the foreign key property
            .IsRequired(false); // Organization is optional

        // Configure the primary key for Organization
        modelBuilder.Entity<Organization>()
            .HasKey(o => o.Id);
    }

}
