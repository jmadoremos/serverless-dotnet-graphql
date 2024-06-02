namespace GraphQL.Database.Repositories.SessionAttendees;

using GraphQL.Database.Repositories.Attendees;
using GraphQL.Database.Repositories.Sessions;
using System.ComponentModel.DataAnnotations.Schema;

[Table("SessionAttendeeMapping")]
public class SessionAttendeeModel : SessionAttendeeModelInput
{
    public SessionModel Session { get; set; } = default!;

    public AttendeeModel Attendee { get; set; } = default!;

    public static SessionAttendeeModel MapFrom(SessionAttendeeModelInput i) => new()
    {
        SessionId = i.SessionId,
        AttendeeId = i.AttendeeId
    };
}
