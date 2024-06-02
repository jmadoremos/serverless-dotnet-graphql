namespace GraphQL.Database.Schemas.Attendees;

using GraphQL.Database.Exceptions;
using GraphQL.Database.Repositories.Attendees;

[ExtendObjectType("Mutation")]
public class AttendeeMutation([Service] IAttendeeRepository attendees)
{
    [GraphQLDescription("Adds an attendee resource.")]
    public async Task<Attendee> AddAttendeeAsync(
        AddAttendeeInput input,
        CancellationToken ctx)
    {
        var attendee = AttendeeModelInput.MapFrom(input);

        var id = await attendees.CreateAttendeeAsync(attendee, ctx);

        var entity = await attendees.GetAttendeeByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(AttendeeModel.Id));

        return Attendee.MapFrom(entity);
    }

    [GraphQLDescription("Updates an attendee resource.")]
    public async Task<Attendee> UpdateAttendeeAsync(
        [GraphQLType(typeof(IdType))] int id,
        UpdateAttendeeInput input,
        CancellationToken ctx)
    {
        var entity = await attendees.GetAttendeeByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(AttendeeModel.Id));

        var attendee = AttendeeModelInput.MapFrom(entity, input);

        await attendees.UpdateAttendeeAsync(id, attendee, ctx);

        entity = await attendees.GetAttendeeByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(AttendeeModel.Id));

        return Attendee.MapFrom(entity);
    }

    [GraphQLDescription("Deletes an attendee resource.")]
    public async Task<Attendee> DeleteAttendeeAsync(
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        var entity = await attendees.GetAttendeeByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(AttendeeModel.Id));

        await attendees.DeleteAttendeeAsync(id, ctx);

        return Attendee.MapFrom(entity);
    }
}
