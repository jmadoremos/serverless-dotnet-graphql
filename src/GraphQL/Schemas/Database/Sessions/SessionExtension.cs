namespace GraphQL.Schemas.Database.Sessions;

using GraphQL.Repositories.Database.Tracks;
using GraphQL.Schemas.Database.Tracks;

[ExtendObjectType(typeof(SessionSchema))]
public class SessionExtension([Service] ITrackRepository tracks)
{
    public async Task<TrackSchema?> GetTrackAsync(
        [Parent] SessionSchema parent,
        CancellationToken ctx)
    {
        var response = await tracks.GetByIdAsync(parent.TrackId, ctx);

        if (response is null)
        {
            return null;
        }

        return TrackSchema.MapFrom(response);
    }
}
