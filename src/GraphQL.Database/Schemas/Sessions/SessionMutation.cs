namespace GraphQL.Database.Schemas.Sessions;

using GraphQL.Database.Exceptions;
using GraphQL.Database.Repositories.SessionAttendees;
using GraphQL.Database.Repositories.Sessions;
using GraphQL.Database.Repositories.SessionSpeakers;

[ExtendObjectType("Mutation")]
public class SessionMutation(
    [Service] ISessionRepository sessions,
    [Service] ISessionAttendeeRepository sessionAttendees,
    [Service] ISessionSpeakerRepository sessionSpeakers)
{
    [GraphQLDescription("Adds a session resource.")]
    public async Task<Session> AddSessionAsync(
        AddSessionInput input,
        CancellationToken ctx)
    {
        var attendee = SessionModelInput.MapFrom(input);

        var id = await sessions.CreateSessionAsync(attendee, ctx);

        var entity = await sessions.GetSessionByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        return Session.MapFrom(entity);
    }

    [GraphQLDescription("Updates a session resource.")]
    public async Task<Session> UpdateSessionAsync(
        [ID(nameof(Session))] int id,
        UpdateSessionInput input,
        CancellationToken ctx)
    {
        var entity = await sessions.GetSessionByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        var attendee = SessionModelInput.MapFrom(entity, input);

        await sessions.UpdateSessionAsync(id, attendee, ctx);

        entity = await sessions.GetSessionByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        return Session.MapFrom(entity);
    }

    [GraphQLDescription("Deletes a session resource.")]
    public async Task<Session> DeleteSessionAsync(
        [ID(nameof(Session))] int id,
        CancellationToken ctx)
    {
        var entity = await sessions.GetSessionByIdAsync(id, ctx)
            ?? throw new SessionNotFoundException();

        await sessions.DeleteSessionAsync(id, ctx);

        return Session.MapFrom(entity);
    }

    [GraphQLDescription("Adds a speaker resource to a session.")]
    public async Task<AddRemoveSessionSpeaker> AddSessionSpeakerAsync(
        AddRemoveSessionSpeaker input,
        CancellationToken ctx)
    {
        var sessionSpeaker = SessionSpeakerModelInput.MapFrom(input);

        await sessionSpeakers.CreateSessionSpeakerAsync(sessionSpeaker, ctx);

        return input;
    }

    [GraphQLDescription("Adds an attendee resource to a session.")]
    public async Task<AddRemoveSessionAttendee> AddSessionAttendeeAsync(
        AddRemoveSessionAttendee input,
        CancellationToken ctx)
    {
        var sessionAttendee = SessionAttendeeModelInput.MapFrom(input);

        await sessionAttendees.CreateSessionAttendeeAsync(sessionAttendee, ctx);

        return input;
    }

    [GraphQLDescription("Removes a speaker resource from a session.")]
    public async Task<AddRemoveSessionSpeaker> RemoveSessionSpeakerAsync(
        AddRemoveSessionSpeaker input,
        CancellationToken ctx)
    {
        var sessionSpeaker = SessionSpeakerModelInput.MapFrom(input);

        await sessionSpeakers.DeleteSessionSpeakerAsync(sessionSpeaker, ctx);

        return input;
    }

    [GraphQLDescription("Removes an attendee resource from a session.")]
    public async Task<AddRemoveSessionAttendee> RemoveSessionAttendeeAsync(
        AddRemoveSessionAttendee input,
        CancellationToken ctx)
    {
        var sessionAttendee = SessionAttendeeModelInput.MapFrom(input);

        await sessionAttendees.DeleteSessionAttendeeAsync(sessionAttendee, ctx);

        return input;
    }
}
