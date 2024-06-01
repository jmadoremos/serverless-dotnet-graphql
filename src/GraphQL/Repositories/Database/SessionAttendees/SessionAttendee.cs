namespace GraphQL.Repositories.Database.SessionAttendees;

using System.ComponentModel.DataAnnotations.Schema;
using GraphQL.Repositories.Database.Attendees;
using GraphQL.Repositories.Database.Sessions;

[Table("SessionAttendeeMapping")]
public class SessionAttendee : SessionAttendeeInput
{
    public Session Session { get; set; } = default!;

    public Attendee Attendee { get; set; } = default!;

    public static SessionAttendee MapFrom(SessionAttendeeInput i) => new()
    {
        SessionId = i.SessionId,
        AttendeeId = i.AttendeeId
    };
}
