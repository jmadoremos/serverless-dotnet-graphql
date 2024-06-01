namespace GraphQL.Schemas.Database.Speakers;

using GraphQL.Repositories.Database.SessionSpeakers;
using GraphQL.Schemas.Database.Sessions;

[ExtendObjectType(typeof(SpeakerSchema))]
public class SpeakerExtension([Service] ISessionSpeakerRepository sessionSpeakers)
{
    public async Task<IEnumerable<SessionSchema>> GetSessionsAsync(
        [Parent] SpeakerSchema parent,
        CancellationToken ctx)
    {
        var response = await sessionSpeakers.GetSessionsBySpeakerAsync(parent.Id, ctx);

        return response.Select(SessionSchema.MapFrom);
    }
}
