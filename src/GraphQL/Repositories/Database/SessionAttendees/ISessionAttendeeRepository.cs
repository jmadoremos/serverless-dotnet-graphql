namespace GraphQL.Repositories.Database.SessionAttendees;

using GraphQL.Repositories.Database.Attendees;
using GraphQL.Repositories.Database.Sessions;

public interface ISessionAttendeeRepository
{
    Task<IQueryable<AttendeeModel>> GetAttendeesBySessionAsync(int sessionId, CancellationToken ctx);

    Task<IQueryable<SessionModel>> GetSessionsByAttendeeAsync(int attendeeId, CancellationToken ctx);

    Task CreateSessionAttendeeAsync(SessionAttendeeModelInput input, CancellationToken ctx);

    Task DeleteSessionAttendeeAsync(SessionAttendeeModelInput input, CancellationToken ctx);
}
