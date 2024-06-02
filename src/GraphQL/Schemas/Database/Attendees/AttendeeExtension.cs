namespace GraphQL.Schemas.Database.Attendees;

using GraphQL.Repositories.Database.SessionAttendees;
using GraphQL.Schemas.Database.Sessions;

[ExtendObjectType(typeof(Attendee))]
public class AttendeeExtension([Service] ISessionAttendeeRepository sessionAttendees)
{
    public async Task<IEnumerable<Session>> GetSessionsAsync(
        [Parent] Attendee parent,
        CancellationToken ctx)
    {
        var response = await sessionAttendees.GetSessionsByAttendeeAsync(parent.Id, ctx);

        return response.Select(Session.MapFrom);
    }
}
