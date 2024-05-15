using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ESLBackend.models;

public class ApplicationDbContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
        base(options)
    { }

    // DbSet for Book entity
    //public DbSet<Book> Books { get; set; }

    // DbSet for MeetingRoom entity
    public DbSet<MeetingRoom> MeetingRooms { get; set; }
    public DbSet<BookingRoom> BookingRooms { get; set; }

}
