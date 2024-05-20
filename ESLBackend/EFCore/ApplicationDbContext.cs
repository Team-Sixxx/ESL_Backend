using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using ESLBackend.Models;
using Microsoft.AspNetCore.Identity;
using ESLBackend.models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public DbSet<Templates> Templates { get; set; }
    public DbSet<Templates.Item> Items { get; set; }
    public DbSet<Templates.Upc> Upcs { get; set; }
    public DbSet<MeetingRoom> MeetingRooms { get; set; }

    public DbSet<User> Users { get; set; }
    public DbSet<Organization> Organizations { get; set; }



    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        modelBuilder.Entity<Templates>()
            .HasKey(t => t.Id);

        modelBuilder.Entity<Templates.Item>()
            .HasKey(i => i.Id);

        modelBuilder.Entity<Templates.Upc>()
            .HasKey(u => u.Id);

        modelBuilder.Entity<Templates>()
            .HasMany(t => t.Items)
            .WithOne(i => i.Templates)
            .HasForeignKey(i => i.TemplatesId);

        modelBuilder.Entity<Templates>()
            .HasMany(t => t.Upcs)
            .WithOne(u => u.Templates)
            .HasForeignKey(u => u.TemplatesId);



        modelBuilder.Entity<Organization>()
     .HasKey(o => o.Id);



        modelBuilder.Entity<User>()
            .HasOne(u => u.Organization)
            .WithMany() // Assuming an organization can have multiple users
            .HasForeignKey(u => u.OrganizationId) // Use the foreign key property
            .IsRequired(false); // Organization is optional

    }
}
