namespace GraphQL.Schemas.Database.Attendees;

using GraphQL.Repositories.Database.SessionAttendees;
using GraphQL.Schemas.Database.Sessions;

[ExtendObjectType(typeof(AttendeeSchema))]
public class AttendeeExtension([Service] ISessionAttendeeRepository sessionAttendees)
{
    public async Task<IEnumerable<SessionSchema>> GetSessionsAsync(
        [Parent] AttendeeSchema parent,
        CancellationToken ctx)
    {
        var response = await sessionAttendees.GetSessionsByAttendeeAsync(parent.Id, ctx);

        return response.Select(SessionSchema.MapFrom);
    }
}
