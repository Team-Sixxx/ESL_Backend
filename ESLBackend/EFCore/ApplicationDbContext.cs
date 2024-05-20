using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ESLBackend.models;
using ESLBackend.Models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public new DbSet<User> Users { get; set; }
    public DbSet<Organization> Organizations { get; set; }

    public DbSet<MeetingRoom> MeetingRooms { get; set; }


    public DbSet<Templates> Templates { get; set; }

    public DbSet<Templates.Item> Items { get; set; }

    public DbSet<Templates.Upc> Upcs { get; set; }

    //public DbSet<Templates.upc> Üpc { get; set; }

    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options) { }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure the primary key for User
        modelBuilder.Entity<User>()
            .HasKey(u => u.Id);


        //modelBuilder.Entity<Templates.Item>()
        // .HasNoKey();


        // Configure the optional relationship with Organization
        modelBuilder.Entity<User>()
            .HasOne(u => u.Organization)
            .WithMany() // Assuming an organization can have multiple users
            .HasForeignKey(u => u.OrganizationId) // Use the foreign key property
            .IsRequired(false); // Organization is optional


        modelBuilder.Entity<Templates.Item>()
          .HasKey(i => new { i.ShopCode, i.GoodsCode });

        modelBuilder.Entity<Templates.Upc>()
     .HasKey(i => new { i.GoodsCode });


        // Configure the primary key for Organization
        modelBuilder.Entity<Organization>()
            .HasKey(o => o.Id);
    }

}
