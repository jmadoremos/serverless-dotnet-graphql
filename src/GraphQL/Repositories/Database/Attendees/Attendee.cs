namespace GraphQL.Repositories.Database.Attendees;

using GraphQL.Repositories.Database.Sessions;

public class Attendee : AttendeeInput
{
    public int Id { get; set; }

    public ICollection<Session> Sessions { get; set; } = [];

    public static Attendee MapFrom(AttendeeInput i) => new()
    {
        FirstName = i.FirstName,
        LastName = i.LastName,
        UserName = i.UserName,
        EmailAddress = i.EmailAddress
    };
}
