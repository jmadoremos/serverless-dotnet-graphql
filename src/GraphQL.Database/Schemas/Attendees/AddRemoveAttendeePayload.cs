namespace GraphQL.Database.Schemas.Attendees;

using ErrorOr;
using GraphQL.Database.Repositories.Attendees;

public class AddRemoveAttendeePayload
{
    [GraphQLDescription("The attendee resource added or removed.")]
    public Attendee? Attendee { get; set; } = default!;

    [GraphQLDescription("A list of errors encountered.")]
    public IEnumerable<Error> AttendeeErrors { get; set; } = [];

    public static AddRemoveAttendeePayload MapFrom(int id, AttendeeModelInput i) => new()
    {
        Attendee = Attendee.MapFrom(id, i)
    };

    public static AddRemoveAttendeePayload MapFrom(AttendeeModel m) => new()
    {
        Attendee = Attendee.MapFrom(m)
    };

    public static AddRemoveAttendeePayload MapFrom(IEnumerable<Error> errors) => new()
    {
        AttendeeErrors = errors
    };

    public static AddRemoveAttendeePayload MapFrom(Error error) => new()
    {
        AttendeeErrors = [error]
    };
}
