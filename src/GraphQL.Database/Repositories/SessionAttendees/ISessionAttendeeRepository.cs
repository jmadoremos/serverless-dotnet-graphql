namespace GraphQL.Database.Repositories.SessionAttendees;

using GraphQL.Database.Repositories.Attendees;
using GraphQL.Database.Repositories.Sessions;

public interface ISessionAttendeeRepository
{
    Task<IQueryable<AttendeeModel>> GetAttendeesBySessionAsync(int sessionId, CancellationToken ctx);

    Task<IQueryable<SessionModel>> GetSessionsByAttendeeAsync(int attendeeId, CancellationToken ctx);

    Task CreateSessionAttendeeAsync(SessionAttendeeModelInput input, CancellationToken ctx);

    Task DeleteSessionAttendeeAsync(SessionAttendeeModelInput input, CancellationToken ctx);
}
