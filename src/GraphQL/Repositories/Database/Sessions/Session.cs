namespace GraphQL.Repositories.Database.Sessions;

using GraphQL.Repositories.Database.Attendees;
using GraphQL.Repositories.Database.Speakers;
using GraphQL.Repositories.Database.Tracks;

public class Session : SessionInput
{
    public int Id { get; set; }

    public ICollection<Attendee> Attendees { get; set; } = [];

    public ICollection<Speaker> Speakers { get; set; } = [];

    public Track Track { get; set; } = default!;

    public static Session MapFrom(SessionInput i) => new()
    {
        Title = i.Title,
        Abstract = i.Abstract,
        StartTime = i.StartTime,
        EndTime = i.EndTime,
        TrackId = i.TrackId
    };
}
