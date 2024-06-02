namespace GraphQL.Repositories.Database.SessionAttendees;

using GraphQL.Repositories.Database.Attendees;
using GraphQL.Repositories.Database.Sessions;
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
