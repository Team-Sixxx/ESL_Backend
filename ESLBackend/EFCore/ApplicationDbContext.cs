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
    public DbSet<Booking> BookingRooms { get; set; }

    public DbSet<ESL> ESLs { get; set; }
    public DbSet<BindESL> BindESLs { get; set; }
    public DbSet<Bind> Binds { get; set; }

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
            .HasMany(t => t.Items);
            
           



        modelBuilder.Entity<Templates>()
            .HasMany(t => t.Upcs);
            
          



        modelBuilder.Entity<Organization>()
     .HasKey(o => o.Id);



        modelBuilder.Entity<User>()
            .HasOne(u => u.Organization)
            .WithMany() // Assuming an organization can have multiple users
            .HasForeignKey(u => u.OrganizationId) // Use the foreign key property
            .IsRequired(false); // Organization is optional


        modelBuilder.Entity<ESL>()
          .HasKey(e => e.Id);

        modelBuilder.Entity<BindESL>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<Bind>()
            .HasKey(b => b.Id);

        modelBuilder.Entity<BindESL>()
            .HasOne(b => b.Binds)
            .WithOne()
            .HasForeignKey<BindESL>(b => b.Id)
            .OnDelete(DeleteBehavior.Cascade);



    }

}
