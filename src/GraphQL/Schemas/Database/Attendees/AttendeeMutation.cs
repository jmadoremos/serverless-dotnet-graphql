namespace GraphQL.Schemas.Database.Attendees;

using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.Attendees;

[ExtendObjectType("Mutation")]
public class AttendeeMutation([Service] IAttendeeRepository attendees)
{
    [GraphQLDescription("Adds an attendee resource.")]
    public async Task<AttendeeSchema> AddAttendeeAsync(
        AddAttendeeSchema input,
        CancellationToken ctx)
    {
        var attendee = AttendeeInput.MapFrom(input);

        var id = await attendees.CreateAsync(attendee, ctx);

        var entity = await attendees.GetByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(Attendee.Id));

        return AttendeeSchema.MapFrom(entity);
    }

    [GraphQLDescription("Updates an attendee resource.")]
    public async Task<AttendeeSchema> UpdateAttendeeAsync(
        [GraphQLType(typeof(IdType))] int id,
        UpdateAttendeeSchema input,
        CancellationToken ctx)
    {
        var entity = await attendees.GetByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(Attendee.Id));

        var attendee = AttendeeInput.MapFrom(entity, input);

        await attendees.UpdateAsync(id, attendee, ctx);

        entity = await attendees.GetByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(Attendee.Id));

        return AttendeeSchema.MapFrom(entity);
    }

    [GraphQLDescription("Deletes an attendee resource.")]
    public async Task<AttendeeSchema> DeleteAttendeeAsync(
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        var entity = await attendees.GetByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(Attendee.Id));

        await attendees.DeleteAsync(id, ctx);

        return AttendeeSchema.MapFrom(entity);
    }
}
