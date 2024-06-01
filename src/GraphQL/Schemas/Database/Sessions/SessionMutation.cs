namespace GraphQL.Schemas.Database.Sessions;

using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.SessionAttendees;
using GraphQL.Repositories.Database.Sessions;
using GraphQL.Repositories.Database.SessionSpeakers;

[ExtendObjectType("Mutation")]
public class SessionMutation(
    [Service] ISessionRepository sessions,
    [Service] ISessionAttendeeRepository sessionAttendees,
    [Service] ISessionSpeakerRepository sessionSpeakers)
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

    [GraphQLDescription("Adds a speaker resource to a session.")]
    public async Task<SessionSpeakerSchema> AddSessionSpeakerAsync(
        SessionSpeakerSchema input,
        CancellationToken ctx)
    {
        var sessionSpeaker = SessionSpeakerInput.MapFrom(input);

        await sessionSpeakers.CreateSessionSpeakerAsync(sessionSpeaker, ctx);

        return input;
    }

    [GraphQLDescription("Adds an attendee resource to a session.")]
    public async Task<SessionAttendeeSchema> AddSessionAttendeeAsync(
        SessionAttendeeSchema input,
        CancellationToken ctx)
    {
        var sessionAttendee = SessionAttendeeInput.MapFrom(input);

        await sessionAttendees.CreateSessionAttendeeAsync(sessionAttendee, ctx);

        return input;
    }

    [GraphQLDescription("Removes a speaker resource from a session.")]
    public async Task<SessionSpeakerSchema> RemoveSessionSpeakerAsync(
        SessionSpeakerSchema input,
        CancellationToken ctx)
    {
        var sessionSpeaker = SessionSpeakerInput.MapFrom(input);

        await sessionSpeakers.DeleteSessionSpeakerAsync(sessionSpeaker, ctx);

        return input;
    }

    [GraphQLDescription("Removes an attendee resource from a session.")]
    public async Task<SessionAttendeeSchema> RemoveSessionAttendeeAsync(
        SessionAttendeeSchema input,
        CancellationToken ctx)
    {
        var sessionAttendee = SessionAttendeeInput.MapFrom(input);

        await sessionAttendees.DeleteSessionAttendeeAsync(sessionAttendee, ctx);

        return input;
    }
}
