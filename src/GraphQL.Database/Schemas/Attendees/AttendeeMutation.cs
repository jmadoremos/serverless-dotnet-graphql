namespace GraphQL.Database.Schemas.Attendees;

using GraphQL.Database.Errors;
using GraphQL.Database.Repositories.Attendees;
using HotChocolate.Subscriptions;

[ExtendObjectType("Mutation")]
public class AttendeeMutation(
    [Service] IAttendeeRepository attendees,
    [Service] ITopicEventSender sender)
{
    [GraphQLDescription("Adds an attendee resource.")]
    public async Task<AddRemoveAttendeePayload> AddAttendeeAsync(
        AddAttendeeInput input,
        CancellationToken ctx)
    {
        var attendee = AttendeeModelInput.MapFrom(input);

        var id = await attendees.CreateAttendeeAsync(attendee, ctx);

        if (id.IsError)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(AttendeeSubscription.AttendeeAdded),
                AddRemoveAttendeePayload.MapFrom(id.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(AttendeeSubscription.AttendeeAdded),
            AddRemoveAttendeePayload.MapFrom(id.Value, attendee),
            ctx);
    }

    [GraphQLDescription("Updates an attendee resource.")]
    public async Task<UpdateAttendeePayload> UpdateAttendeeAsync(
        [ID(nameof(Attendee))] int id,
        UpdateAttendeeInput input,
        CancellationToken ctx)
    {
        var entity = await attendees.GetAttendeeByIdAsync(id, ctx);

        if (entity is null)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(AttendeeSubscription.AttendeeUpdated),
                UpdateAttendeePayload.MapFrom(AttendeeError.NotFound(id)),
                ctx);
        }

        var attendee = AttendeeModelInput.MapFrom(entity, input);

        var update = await attendees.UpdateAttendeeAsync(id, attendee, ctx);

        if (update.IsError)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(AttendeeSubscription.AttendeeUpdated),
                UpdateAttendeePayload.MapFrom(entity, update.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(AttendeeSubscription.AttendeeUpdated),
            UpdateAttendeePayload.MapFrom(id, entity, attendee),
            ctx);
    }

    [GraphQLDescription("Removes an attendee resource.")]
    public async Task<AddRemoveAttendeePayload> RemoveAttendeeAsync(
        [ID(nameof(Attendee))] int id,
        CancellationToken ctx)
    {
        var entity = await attendees.GetAttendeeByIdAsync(id, ctx);

        if (entity is null)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(AttendeeSubscription.AttendeeRemoved),
                AddRemoveAttendeePayload.MapFrom(AttendeeError.NotFound(id)),
                ctx);
        }

        var delete = await attendees.DeleteAttendeeAsync(id, ctx);

        if (delete.IsError)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(AttendeeSubscription.AttendeeRemoved),
                AddRemoveAttendeePayload.MapFrom(delete.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(AttendeeSubscription.AttendeeRemoved),
            AddRemoveAttendeePayload.MapFrom(entity),
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
