namespace GraphQL.Database.Repositories.SessionAttendees;

using ErrorOr;
using GraphQL.Database.Repositories.Attendees;
using GraphQL.Database.Repositories.Sessions;

public interface ISessionAttendeeRepository
{
    Task<IQueryable<AttendeeModel>> GetAttendeesBySessionAsync(int sessionId, CancellationToken ctx);

    Task<IQueryable<SessionModel>> GetSessionsByAttendeeAsync(int attendeeId, CancellationToken ctx);

    Task<ErrorOr<Created>> CreateSessionAttendeeAsync(SessionAttendeeModelInput input, CancellationToken ctx);

    Task<ErrorOr<Deleted>> DeleteSessionAttendeeAsync(SessionAttendeeModelInput input, CancellationToken ctx);
}
