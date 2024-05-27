namespace GraphQL.Schemas.Database.Sessions;

using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.Sessions;
using System.Linq;

[ExtendObjectType("Query")]
public class SessionQuery([Service] ISessionRepository sessions)
{
    [GraphQLDescription("A list of sessions of all tracks.")]
    public async Task<IEnumerable<SessionSchema>> GetSessionsAsync(CancellationToken ctx)
    {
        var result = await sessions.GetAllAsync(ctx);
        return result.Select(SessionSchema.MapFrom);
    }

    [GraphQLDescription("A session of any track.")]
    public async Task<SessionSchema> GetSessionAsync(
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        var result = await sessions.GetByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        return SessionSchema.MapFrom(result);
    }
}
