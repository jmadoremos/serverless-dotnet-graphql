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
    public DbSet<Attendee> Attendees { get; set; } = default!;

    public DbSet<Session> Sessions { get; set; } = default!;

    public DbSet<Speaker> Speakers { get; set; } = default!;

    public DbSet<Track> Tracks { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendee>()
            .HasIndex(a => a.UserName)
            .IsUnique();

        modelBuilder.Entity<Session>()
            .HasIndex(a => new { a.Title, a.StartTime, a.EndTime })
            .IsUnique();

        modelBuilder.Entity<Speaker>()
            .HasIndex(a => a.Name)
            .IsUnique();

        modelBuilder.Entity<Track>()
            .HasIndex(a => a.Name)
            .IsUnique();

        // Many-to-many: Session <-> Attendee
        modelBuilder.Entity<SessionAttendeeMapping>()
            .HasKey(ca => new { ca.SessionId, ca.AttendeeId });

        // Many-to-many: Speaker <-> Session
        modelBuilder.Entity<SessionSpeakerMapping>()
            .HasKey(ss => new { ss.SessionId, ss.SpeakerId });
    }
}
