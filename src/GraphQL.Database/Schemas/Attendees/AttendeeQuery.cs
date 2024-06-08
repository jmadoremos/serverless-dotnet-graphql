namespace GraphQL.Database.Schemas.Attendees;

using GraphQL.Database.Repositories.Attendees;
using System.Linq;

[ExtendObjectType("Query")]
public class AttendeeQuery([Service] IAttendeeRepository attendees)
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("A list of attendees from all sessions.")]
    public async Task<IEnumerable<Attendee>> GetAttendeesAsync(CancellationToken ctx)
    {
        var result = await attendees.GetAllAttendeesAsync(ctx);
        return result.Select(Attendee.MapFrom);
    }

    [GraphQLDescription("An attendee of any session.")]
    public async Task<Attendee?> GetAttendeeAsync(
        [ID(nameof(Attendee))] int id,
        CancellationToken ctx)
    {
        var result = await attendees.GetAttendeeByIdAsync(id, ctx);

        if (result is null)
        {
            return null;
        }

        return Attendee.MapFrom(result);
    }
}
