namespace GraphQL.Repositories.Database.Tracks;

using GraphQL.Repositories.Database.Sessions;

public class Track : TrackInput
{
    public int Id { get; set; }

    public ICollection<Session> Sessions { get; set; } = [];

    public static Track MapFrom(TrackInput i) => new()
    {
        Name = i.Name
    };
}
