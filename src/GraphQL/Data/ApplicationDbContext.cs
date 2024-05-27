namespace GraphQL.Data;

using GraphQL.Repositories.Database.Attendees;
using GraphQL.Repositories.Database.SessionAttendeeMapping;
using GraphQL.Repositories.Database.Sessions;
using GraphQL.Repositories.Database.SessionSpeakerMapping;
using GraphQL.Repositories.Database.Speakers;
using GraphQL.Repositories.Database.Tracks;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Speaker> Speakers { get; set; } = default!;

    public DbSet<Attendee> Attendees { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder
            .Entity<Attendee>()
            .HasIndex(a => a.UserName)
            .IsUnique();

        modelBuilder.Entity<Speaker>()
            .HasIndex(a => a.Name)
            .IsUnique();
    }
}
