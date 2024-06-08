namespace GraphQL.Database.Schemas.Sessions;

using GraphQL.Database.Errors;
using GraphQL.Database.Repositories.SessionAttendees;
using GraphQL.Database.Repositories.Sessions;
using GraphQL.Database.Repositories.SessionSpeakers;
using HotChocolate.Subscriptions;

[ExtendObjectType("Mutation")]
public class SessionMutation(
    [Service] ITopicEventSender sender,
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
            return await this.PublishAndReturnPayloadAsync(
                nameof(SessionSubscription.SessionAdded),
                AddRemoveSessionPayload.MapFrom(id.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(SessionSubscription.SessionAdded),
            AddRemoveSessionPayload.MapFrom(id.Value, attendee),
            ctx);
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
            return await this.PublishAndReturnPayloadAsync(
                nameof(SessionSubscription.SessionUpdated),
                UpdateSessionPayload.MapFrom(SessionError.NotFound(id)),
                ctx);
        }

        var session = SessionModelInput.MapFrom(entity, input);

        var update = await sessions.UpdateSessionAsync(id, session, ctx);

        if (update.IsError)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(SessionSubscription.SessionUpdated),
                UpdateSessionPayload.MapFrom(entity, update.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(SessionSubscription.SessionUpdated),
            UpdateSessionPayload.MapFrom(id, entity, session),
            ctx);
    }

    [GraphQLDescription("Removes a session resource.")]
    public async Task<AddRemoveSessionPayload> RemoveSessionAsync(
        [ID(nameof(Session))] int id,
        CancellationToken ctx)
    {
        var entity = await sessions.GetSessionByIdAsync(id, ctx);

        if (entity is null)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(SessionSubscription.SessionRemoved),
                AddRemoveSessionPayload.MapFrom(SessionError.NotFound(id)),
                ctx);
        }

        var delete = await sessions.DeleteSessionAsync(id, ctx);

        if (delete.IsError)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(SessionSubscription.SessionRemoved),
                AddRemoveSessionPayload.MapFrom(delete.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(SessionSubscription.SessionRemoved),
            AddRemoveSessionPayload.MapFrom(entity),
            ctx);
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
            return await this.PublishAndReturnPayloadAsync(
                nameof(SessionSubscription.SessionSpeakerAdded),
                AddRemoveSessionSpeakerPayload.MapFrom(create.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(SessionSubscription.SessionSpeakerAdded),
            new AddRemoveSessionSpeakerPayload(),
            ctx);
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
            return await this.PublishAndReturnPayloadAsync(
                nameof(SessionSubscription.SessionAttendeeAdded),
                AddRemoveSessionAttendeePayload.MapFrom(create.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(SessionSubscription.SessionAttendeeAdded),
            new AddRemoveSessionAttendeePayload(),
            ctx);
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
            return await this.PublishAndReturnPayloadAsync(
                nameof(SessionSubscription.SessionSpeakerRemoved),
                AddRemoveSessionSpeakerPayload.MapFrom(delete.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(SessionSubscription.SessionSpeakerRemoved),
            new AddRemoveSessionSpeakerPayload(),
            ctx);
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
            return await this.PublishAndReturnPayloadAsync(
                nameof(SessionSubscription.SessionAttendeeRemoved),
                AddRemoveSessionAttendeePayload.MapFrom(delete.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
                nameof(SessionSubscription.SessionAttendeeRemoved),
                new AddRemoveSessionAttendeePayload(),
                ctx);
    }

    private async Task<TPayload> PublishAndReturnPayloadAsync<TPayload>(
        string eventName,
        TPayload payload,
        CancellationToken ctx)
    {
        await sender.SendAsync(eventName, payload, ctx);

        return payload;
    }
}
