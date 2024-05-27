namespace GraphQL.Schemas.Database.Sessions;

using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.Sessions;

[ExtendObjectType("Mutation")]
public class SessionMutation([Service] ISessionRepository sessions)
{
    [GraphQLDescription("Adds a session resource.")]
    public async Task<SessionSchema> AddSessionAsync(
        AddSessionSchema input,
        CancellationToken ctx)
    {
        var attendee = SessionInput.MapFrom(input);

        var id = await sessions.CreateAsync(attendee, ctx);

        var entity = await sessions.GetByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        return SessionSchema.MapFrom(entity);
    }

    [GraphQLDescription("Updates a session resource.")]
    public async Task<SessionSchema> UpdateSessionAsync(
        [GraphQLType(typeof(IdType))] int id,
        UpdateSessionSchema input,
        CancellationToken ctx)
    {
        var entity = await sessions.GetByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        var attendee = SessionInput.MapFrom(entity, input);

        await sessions.UpdateAsync(id, attendee, ctx);

        entity = await sessions.GetByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        return SessionSchema.MapFrom(entity);
    }

    [GraphQLDescription("Deletes a session resource.")]
    public async Task<SessionSchema> DeleteSessionAsync(
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        var entity = await sessions.GetByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        await sessions.DeleteAsync(id, ctx);

        return SessionSchema.MapFrom(entity);
    }
}
