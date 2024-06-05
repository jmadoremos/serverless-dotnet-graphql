namespace GraphQL.Database.Schemas.Sessions;

using GraphQL.Database.Errors;
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
    public async Task<AddRemoveSessionPayload> AddSessionAsync(
        AddSessionInput input,
        CancellationToken ctx)
    {
        var attendee = SessionModelInput.MapFrom(input);

        var id = await sessions.CreateSessionAsync(attendee, ctx);

        if (id.IsError)
        {
            return AddRemoveSessionPayload.MapFrom(id.Errors);
        }

        return AddRemoveSessionPayload.MapFrom(id.Value, attendee);
    }

    [GraphQLDescription("Updates a session resource.")]
    public async Task<UpdateSessionPayload> UpdateSessionAsync(
        [ID(nameof(Session))] int id,
        UpdateSessionInput input,
        CancellationToken ctx)
    {
        var entity = await sessions.GetSessionByIdAsync(id, ctx);

        if (entity is null)
        {
            return UpdateSessionPayload.MapFrom(SessionError.NotFound(id));
        }

        var session = SessionModelInput.MapFrom(entity, input);

        var update = await sessions.UpdateSessionAsync(id, session, ctx);

        if (update.IsError)
        {
            return UpdateSessionPayload.MapFrom(entity, update.Errors);
        }

        return UpdateSessionPayload.MapFrom(id, entity, session);
    }

    [GraphQLDescription("Removes a session resource.")]
    public async Task<AddRemoveSessionPayload> RemoveSessionAsync(
        [ID(nameof(Session))] int id,
        CancellationToken ctx)
    {
        var entity = await sessions.GetSessionByIdAsync(id, ctx);

        if (entity is null)
        {
            return AddRemoveSessionPayload.MapFrom(SessionError.NotFound(id));
        }

        var delete = await sessions.DeleteSessionAsync(id, ctx);

        if (delete.IsError)
        {
            return AddRemoveSessionPayload.MapFrom(delete.Errors);
        }

        return AddRemoveSessionPayload.MapFrom(entity);
    }

    [GraphQLDescription("Adds a speaker resource to a session.")]
    public async Task<AddRemoveSessionSpeakerPayload> AddSessionSpeakerAsync(
        AddRemoveSessionSpeaker input,
        CancellationToken ctx)
    {
        var sessionSpeaker = SessionSpeakerModelInput.MapFrom(input);

        var create = await sessionSpeakers.CreateSessionSpeakerAsync(sessionSpeaker, ctx);

        if (create.IsError)
        {
            return AddRemoveSessionSpeakerPayload.MapFrom(create.Errors);
        }

        return new AddRemoveSessionSpeakerPayload();
    }

    [GraphQLDescription("Adds an attendee resource to a session.")]
    public async Task<AddRemoveSessionAttendeePayload> AddSessionAttendeeAsync(
        AddRemoveSessionAttendee input,
        CancellationToken ctx)
    {
        var sessionAttendee = SessionAttendeeModelInput.MapFrom(input);

        var create = await sessionAttendees.CreateSessionAttendeeAsync(sessionAttendee, ctx);

        if (create.IsError)
        {
            return AddRemoveSessionAttendeePayload.MapFrom(create.Errors);
        }

        return new AddRemoveSessionAttendeePayload();
    }

    [GraphQLDescription("Removes a speaker resource from a session.")]
    public async Task<AddRemoveSessionSpeakerPayload> RemoveSessionSpeakerAsync(
        AddRemoveSessionSpeaker input,
        CancellationToken ctx)
    {
        var sessionSpeaker = SessionSpeakerModelInput.MapFrom(input);

        var delete = await sessionSpeakers.DeleteSessionSpeakerAsync(sessionSpeaker, ctx);

        if (delete.IsError)
        {
            return AddRemoveSessionSpeakerPayload.MapFrom(delete.Errors);
        }

        return new AddRemoveSessionSpeakerPayload();
    }

    [GraphQLDescription("Removes an attendee resource from a session.")]
    public async Task<AddRemoveSessionAttendeePayload> RemoveSessionAttendeeAsync(
        AddRemoveSessionAttendee input,
        CancellationToken ctx)
    {
        var sessionAttendee = SessionAttendeeModelInput.MapFrom(input);

        var delete = await sessionAttendees.DeleteSessionAttendeeAsync(sessionAttendee, ctx);

        if (delete.IsError)
        {
            return AddRemoveSessionAttendeePayload.MapFrom(delete.Errors);
        }

        return new AddRemoveSessionAttendeePayload();
    }
}
