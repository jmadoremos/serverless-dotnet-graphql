namespace GraphQL.Schemas.Database.Speakers;

using GraphQL.Repositories.Database.Tracks;
using GraphQL.Schemas.Database.Sessions;
using GraphQL.Schemas.Database.Tracks;

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
