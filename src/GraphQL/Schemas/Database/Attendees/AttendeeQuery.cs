namespace GraphQL.Schemas.Database.Attendees;

using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.Attendees;
using System.Linq;

[ExtendObjectType("Query")]
public class AttendeeQuery([Service] IAttendeeRepository attendees)
{
    [GraphQLDescription("A list of attendees from all sessions.")]
    public async Task<IEnumerable<Attendee>> GetAttendeesAsync(CancellationToken ctx)
    {
        var result = await attendees.GetAllAttendeesAsync(ctx);
        return result.Select(Attendee.MapFrom);
    }

    [GraphQLDescription("An attendee of any session.")]
    public async Task<Attendee> GetAttendeeAsync(
        [ID(nameof(Attendee))] int id,
        CancellationToken ctx)
    {
        var result = await attendees.GetAttendeeByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(Attendee.Id));

        return Attendee.MapFrom(result);
    }
}
