namespace GraphQL.Schemas.Database.Speakers;

using GraphQL.Repositories.Database.SessionSpeakers;
using GraphQL.Schemas.Database.Sessions;

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
