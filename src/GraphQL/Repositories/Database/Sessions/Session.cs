namespace GraphQL.Repositories.Database.Sessions;

using GraphQL.Repositories.Database.SessionAttendeeMapping;
using GraphQL.Repositories.Database.SessionSpeakerMapping;
using GraphQL.Repositories.Database.Tracks;

public class Session : SessionInput
{
    public int Id { get; set; }

    public ICollection<SessionAttendeeMapping> Attendees { get; set; } = [];

    public ICollection<SessionSpeakerMapping> Speakers { get; set; } = [];

    public Track? Track { get; set; }

    public static Session MapFrom(SessionInput i) => new()
    {
        Title = i.Title,
        Abstract = i.Abstract,
        StartTime = i.StartTime,
        EndTime = i.EndTime,
        TrackId = i.TrackId
    };
}
