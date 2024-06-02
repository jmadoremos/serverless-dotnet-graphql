namespace GraphQL.Database.Schemas.Speakers;

using GraphQL.Database.Repositories.Tracks;
using GraphQL.Database.Schemas.Sessions;
using GraphQL.Database.Schemas.Tracks;

[ExtendObjectType(typeof(Track))]
public class TrackExtension([Service] ITrackRepository tracks)
{
    public async Task<IEnumerable<Session>> GetSessionsAsync(
        [Parent] Track parent,
        CancellationToken ctx)
    {
        var response = await tracks.GetSessionsAsync(parent.Id, ctx);

        return response.Select(Session.MapFrom);
    }
}
