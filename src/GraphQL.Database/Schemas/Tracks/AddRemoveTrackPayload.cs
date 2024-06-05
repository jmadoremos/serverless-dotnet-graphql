namespace GraphQL.Database.Schemas.Tracks;

using ErrorOr;
using GraphQL.Database.Repositories.Tracks;

public class AddRemoveTrackPayload
{
    [GraphQLDescription("The track resource added or removed.")]
    public Track? Track { get; set; } = default!;

    [GraphQLDescription("A list of errors encountered.")]
    public IEnumerable<Error> TrackErrors { get; set; } = [];

    public static AddRemoveTrackPayload MapFrom(int id, TrackModelInput i) => new()
    {
        Track = Track.MapFrom(id, i)
    };

    public static AddRemoveTrackPayload MapFrom(TrackModel m) => new()
    {
        Track = Track.MapFrom(m)
    };

    public static AddRemoveTrackPayload MapFrom(IEnumerable<Error> errors) => new()
    {
        TrackErrors = errors
    };

    public static AddRemoveTrackPayload MapFrom(Error error) => new()
    {
        TrackErrors = [error]
    };
}
