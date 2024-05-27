namespace GraphQL.Schemas.Database.Tracks;

using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.Tracks;
using System.Linq;

[ExtendObjectType("Query")]
public class TrackQuery([Service] ITrackRepository tracks)
{
    [GraphQLDescription("A list of tracks.")]
    public async Task<IEnumerable<TrackSchema>> GetTracksAsync(CancellationToken ctx)
    {
        var result = await tracks.GetAllAsync(ctx);
        return result.Select(TrackSchema.MapFrom);
    }

    [GraphQLDescription("A track.")]
    public async Task<TrackSchema> GetTrackAsync(
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        var result = await tracks.GetByIdAsync(id, ctx)
            ?? throw new TrackNotFoundException();

        return TrackSchema.MapFrom(result);
    }
}
