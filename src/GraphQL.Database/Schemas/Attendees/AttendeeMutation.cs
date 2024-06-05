namespace GraphQL.Database.Schemas.Attendees;

using GraphQL.Database.Errors;
using GraphQL.Database.Repositories.Attendees;

[ExtendObjectType("Mutation")]
public class AttendeeMutation([Service] IAttendeeRepository attendees)
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
            return AddRemoveAttendeePayload.MapFrom(id.Errors);
        }

        return AddRemoveAttendeePayload.MapFrom(id.Value, attendee);
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
            return UpdateAttendeePayload.MapFrom(AttendeeError.NotFound(id));
        }

        var attendee = AttendeeModelInput.MapFrom(entity, input);

        var update = await attendees.UpdateAttendeeAsync(id, attendee, ctx);

        if (update.IsError)
        {
            return UpdateAttendeePayload.MapFrom(entity, update.Errors);
        }

        return UpdateAttendeePayload.MapFrom(id, entity, attendee);
    }

    [GraphQLDescription("Removes an attendee resource.")]
    public async Task<AddRemoveAttendeePayload> RemoveAttendeeAsync(
        [ID(nameof(Attendee))] int id,
        CancellationToken ctx)
    {
        var entity = await attendees.GetAttendeeByIdAsync(id, ctx);

        if (entity is null)
        {
            return AddRemoveAttendeePayload.MapFrom(AttendeeError.NotFound(id));
        }

        var delete = await attendees.DeleteAttendeeAsync(id, ctx);

        if (delete.IsError)
        {
            return AddRemoveAttendeePayload.MapFrom(delete.Errors);
        }

        return AddRemoveAttendeePayload.MapFrom(entity);
    }
}
