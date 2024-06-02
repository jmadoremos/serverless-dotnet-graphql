namespace GraphQL.Repositories.Database.SessionAttendees;

using GraphQL.Schemas.Database.Sessions;

public class SessionAttendeeModelInput
{
    public int SessionId { get; set; }

    public int AttendeeId { get; set; }

    public static SessionAttendeeModelInput MapFrom(AddRemoveSessionAttendee s) => new()
    {
        SessionId = s.SessionId,
        AttendeeId = s.AttendeeId
    };
}
