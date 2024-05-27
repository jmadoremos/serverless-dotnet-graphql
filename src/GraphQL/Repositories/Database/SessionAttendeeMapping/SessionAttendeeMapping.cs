namespace GraphQL.Repositories.Database.SessionAttendeeMapping;

using GraphQL.Repositories.Database.Attendees;
using GraphQL.Repositories.Database.Sessions;

public class SessionAttendeeMapping
{
    public int SessionId { get; set; }

    public Session? Session { get; set; }

    public int AttendeeId { get; set; }

    public Attendee? Attendee { get; set; }
}
