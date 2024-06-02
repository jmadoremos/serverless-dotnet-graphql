namespace GraphQL.Database.Schemas.Speakers;

using GraphQL.Database.Repositories.SessionSpeakers;
using GraphQL.Database.Schemas.Sessions;

[ExtendObjectType(typeof(Speaker))]
public class SpeakerExtension([Service] ISessionSpeakerRepository sessionSpeakers)
{
    public async Task<IEnumerable<Session>> GetSessionsAsync(
        [Parent] Speaker parent,
        CancellationToken ctx)
    {
        var response = await sessionSpeakers.GetSessionsBySpeakerAsync(parent.Id, ctx);

        return response.Select(Session.MapFrom);
    }
}
