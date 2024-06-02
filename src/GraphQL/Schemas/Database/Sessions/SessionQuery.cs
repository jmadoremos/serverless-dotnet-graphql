namespace GraphQL.Schemas.Database.Sessions;

using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.Sessions;
using System.Linq;

[ExtendObjectType("Query")]
public class SessionQuery([Service] ISessionRepository sessions)
{
    [GraphQLDescription("A list of sessions of all tracks.")]
    public async Task<IEnumerable<Session>> GetSessionsAsync(CancellationToken ctx)
    {
        var result = await sessions.GetAllSessionsAsync(ctx);
        return result.Select(Session.MapFrom);
    }

    [GraphQLDescription("A session of any track.")]
    public async Task<Session> GetSessionAsync(
        [ID(nameof(Session))] int id,
        CancellationToken ctx)
    {
        var result = await sessions.GetSessionByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        return Session.MapFrom(result);
    }
}
