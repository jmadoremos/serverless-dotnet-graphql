namespace GraphQL.Database.Schemas.Tracks;

using ErrorOr;
using GraphQL.Database.Repositories.Tracks;

public class UpdateTrackPayload
{
    [GraphQLDescription("The track resource before the changes.")]
    public Track? BeforeTrack { get; set; } = default!;

    [GraphQLDescription("The track resource after the changes.")]
    public Track? AfterTrack { get; set; } = default!;

    [GraphQLDescription("A list of errors encountered.")]
    public IEnumerable<Error> TrackErrors { get; set; } = [];

    public static UpdateTrackPayload MapFrom(int id, TrackModel m, TrackModelInput i) => new()
    {
        BeforeTrack = Track.MapFrom(m),
        AfterTrack = Track.MapFrom(id, i)
    };

    public static UpdateTrackPayload MapFrom(TrackModel m, IEnumerable<Error> errors) => new()
    {
        BeforeTrack = Track.MapFrom(m),
        TrackErrors = errors
    };

    public static UpdateTrackPayload MapFrom(Error error) => new()
    {
        TrackErrors = [error]
    };
}
