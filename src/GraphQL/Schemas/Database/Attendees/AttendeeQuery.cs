namespace GraphQL.Schemas.Database.Attendees;

using GraphQL.Repositories.Database.Attendees;
using System.Linq;

[ExtendObjectType("Query")]
public class AttendeeQuery([Service] IAttendeeRepository attendees)
{
    [GraphQLDescription("A list of attendees from all sessions.")]
    public async Task<IEnumerable<AttendeeSchema>> GetAttendeesAsync(CancellationToken ctx)
    {
        var result = await attendees.GetAllAsync(ctx);
        return result.Select(AttendeeSchema.MapFrom);
    }

    [GraphQLDescription("An attendee of any session.")]
    public async Task<AttendeeSchema> GetAttendeeAsync(
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        var result = await attendees.GetByIdAsync(id, ctx);
        return AttendeeSchema.MapFrom(result);
    }
}
