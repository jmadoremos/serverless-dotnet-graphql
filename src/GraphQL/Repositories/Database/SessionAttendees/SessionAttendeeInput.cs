namespace GraphQL.Repositories.Database.SessionAttendees;

using GraphQL.Schemas.Database.Sessions;

public class SessionAttendeeInput
{
    public int SessionId { get; set; }

    public int AttendeeId { get; set; }

    public static SessionAttendeeInput MapFrom(SessionAttendeeSchema s) => new()
    {
        SessionId = s.SessionId,
        AttendeeId = s.AttendeeId
    };
}
