namespace GraphQL.Database.Schemas.Tracks;

using GraphQL.Database.Repositories.Tracks;
using System.Linq;

[ExtendObjectType("Query")]
public class TrackQuery([Service] ITrackRepository tracks)
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("A list of tracks.")]
    public async Task<IEnumerable<Track>> GetTracksAsync(CancellationToken ctx)
    {
        var result = await tracks.GetAllTracksAsync(ctx);
        return result.Select(Track.MapFrom);
    }

    [GraphQLDescription("A track.")]
    public async Task<Track?> GetTrackAsync(
        [ID(nameof(Track))] int id,
        CancellationToken ctx)
    {
        var result = await tracks.GetTrackByIdAsync(id, ctx);

        if (result is null)
        {
            return null;
        }

        return Track.MapFrom(result);
    }
}
