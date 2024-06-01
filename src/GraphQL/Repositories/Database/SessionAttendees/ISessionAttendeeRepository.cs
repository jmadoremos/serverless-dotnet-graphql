namespace GraphQL.Repositories.Database.SessionAttendees;

using GraphQL.Repositories.Database.Attendees;
using GraphQL.Repositories.Database.Sessions;

public interface ISessionAttendeeRepository
{
    Task<IQueryable<Attendee>> GetAttendeesBySessionAsync(int sessionId, CancellationToken ctx);

    Task<IQueryable<Session>> GetSessionsByAttendeeAsync(int attendeeId, CancellationToken ctx);

    Task CreateSessionAttendeeAsync(SessionAttendeeInput input, CancellationToken ctx);

    Task DeleteSessionAttendeeAsync(SessionAttendeeInput input, CancellationToken ctx);
}
