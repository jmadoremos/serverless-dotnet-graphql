namespace GraphQL.Database;

using GraphQL.Database.Repositories.Attendees;
using GraphQL.Database.Repositories.SessionAttendees;
using GraphQL.Database.Repositories.Sessions;
using GraphQL.Database.Repositories.SessionSpeakers;
using GraphQL.Database.Repositories.Speakers;
using GraphQL.Database.Repositories.Tracks;
using Microsoft.EntityFrameworkCore;

public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : DbContext(options)
{
    public DbSet<AttendeeModel> Attendees { get; set; } = default!;

    public DbSet<SessionModel> Sessions { get; set; } = default!;

    public DbSet<SessionAttendeeModel> SessionAttendees { get; set; } = default!;

    public DbSet<SessionSpeakerModel> SessionSpeakers { get; set; } = default!;

    public DbSet<SpeakerModel> Speakers { get; set; } = default!;

    public DbSet<TrackModel> Tracks { get; set; } = default!;

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<AttendeeModel>()
            .HasIndex(e => e.UserName)
            .IsUnique();

        modelBuilder.Entity<SessionModel>()
            .HasIndex(e => e.Title)
            .IsUnique();

        modelBuilder.Entity<SpeakerModel>()
            .HasIndex(e => e.Name)
            .IsUnique();

        modelBuilder.Entity<TrackModel>()
            .HasIndex(e => e.Name)
            .IsUnique();

        // One-to-one: Session <-> Track
        modelBuilder.Entity<TrackModel>()
            .HasMany(e => e.Sessions)
            .WithOne(e => e.Track);

        // Many-to-many: Session <-> Attendee
        modelBuilder.Entity<SessionAttendeeModel>()
            .HasKey(e => new { e.SessionId, e.AttendeeId });

        modelBuilder.Entity<SessionModel>()
            .HasMany(e => e.Attendees)
            .WithMany(e => e.Sessions)
            .UsingEntity<SessionAttendeeModel>();

        // Many-to-many: Speaker <-> Session
        modelBuilder.Entity<SessionSpeakerModel>()
            .HasKey(e => new { e.SessionId, e.SpeakerId });

        modelBuilder.Entity<SessionModel>()
            .HasMany(e => e.Speakers)
            .WithMany(e => e.Sessions)
            .UsingEntity<SessionSpeakerModel>();
    }
}
