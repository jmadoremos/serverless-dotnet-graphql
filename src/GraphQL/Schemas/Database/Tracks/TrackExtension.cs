namespace GraphQL.Schemas.Database.Speakers;

using GraphQL.Repositories.Database.Tracks;
using GraphQL.Schemas.Database.Sessions;
using GraphQL.Schemas.Database.Tracks;

[ExtendObjectType(typeof(TrackSchema))]
public class TrackExtension([Service] ITrackRepository tracks)
{
    public async Task<IEnumerable<SessionSchema>> GetSessionsAsync(
        [Parent] TrackSchema parent,
        CancellationToken ctx)
    {
        var response = await tracks.GetSessionsAsync(parent.Id, ctx);

        return response.Select(SessionSchema.MapFrom);
    }
}
