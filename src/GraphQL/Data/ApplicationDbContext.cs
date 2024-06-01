namespace GraphQL.Data;

using GraphQL.Repositories.Database.Attendees;
using GraphQL.Repositories.Database.SessionAttendees;
using GraphQL.Repositories.Database.Sessions;
using GraphQL.Repositories.Database.SessionSpeakers;
using GraphQL.Repositories.Database.Speakers;
using GraphQL.Repositories.Database.Tracks;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<Attendee> Attendees { get; set; } = default!;

    public DbSet<Session> Sessions { get; set; } = default!;

    public DbSet<SessionAttendee> SessionAttendees { get; set; } = default!;

    public DbSet<SessionSpeaker> SessionSpeakers { get; set; } = default!;

    public DbSet<Speaker> Speakers { get; set; } = default!;

    public DbSet<Track> Tracks { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<Attendee>()
            .HasIndex(e => e.UserName)
            .IsUnique();

        modelBuilder.Entity<Session>()
            .HasIndex(e => e.Title)
            .IsUnique();

        modelBuilder.Entity<Speaker>()
            .HasIndex(e => e.Name)
            .IsUnique();

        modelBuilder.Entity<Track>()
            .HasIndex(e => e.Name)
            .IsUnique();

        // One-to-one: Session <-> Track
        modelBuilder.Entity<Track>()
            .HasMany(e => e.Sessions)
            .WithOne(e => e.Track);

        // Many-to-many: Session <-> Attendee
        modelBuilder.Entity<SessionAttendee>()
            .HasKey(e => new { e.SessionId, e.AttendeeId });

        modelBuilder.Entity<Session>()
            .HasMany(e => e.Attendees)
            .WithMany(e => e.Sessions)
            .UsingEntity<SessionAttendee>();

        // Many-to-many: Speaker <-> Session
        modelBuilder.Entity<SessionSpeaker>()
            .HasKey(e => new { e.SessionId, e.SpeakerId });

        modelBuilder.Entity<Session>()
            .HasMany(e => e.Speakers)
            .WithMany(e => e.Sessions)
            .UsingEntity<SessionSpeaker>();
    }
}
