namespace GraphQL.Repositories.Database.Attendees;

using GraphQL.Repositories.Database.SessionAttendeeMapping;

public class Attendee : AttendeeInput
{
    public int Id { get; set; }

    public ICollection<SessionAttendeeMapping> SessionsAttendees { get; set; } = [];

    public static Attendee MapFrom(AttendeeInput i) => new()
    {
        FirstName = i.FirstName,
        LastName = i.LastName,
        UserName = i.UserName,
        EmailAddress = i.EmailAddress
    };
}
