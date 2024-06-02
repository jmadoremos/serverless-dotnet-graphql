namespace GraphQL.Schemas.Database.Tracks;

using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.Tracks;
using System.Linq;

[ExtendObjectType("Query")]
public class TrackQuery([Service] ITrackRepository tracks)
{
    [GraphQLDescription("A list of tracks.")]
    public async Task<IEnumerable<Track>> GetTracksAsync(CancellationToken ctx)
    {
        var result = await tracks.GetAllTracksAsync(ctx);
        return result.Select(Track.MapFrom);
    }

    [GraphQLDescription("A track.")]
    public async Task<Track> GetTrackAsync(
        [ID(nameof(Track))] int id,
        CancellationToken ctx)
    {
        var result = await tracks.GetTrackByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        return Track.MapFrom(result);
    }
}
