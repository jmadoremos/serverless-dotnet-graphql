namespace GraphQL.Database.Schemas.Attendees;

using ErrorOr;
using GraphQL.Database.Repositories.Attendees;

public class UpdateAttendeePayload
{
    [GraphQLDescription("The attendee resource before the changes.")]
    public Attendee? BeforeAttendee { get; set; } = default!;

    [GraphQLDescription("The attendee resource after the changes.")]
    public Attendee? AfterAttendee { get; set; } = default!;

    [GraphQLDescription("A list of errors encountered.")]
    public IEnumerable<Error> AttendeeErrors { get; set; } = [];

    public static UpdateAttendeePayload MapFrom(int id, AttendeeModel m, AttendeeModelInput i) => new()
    {
        BeforeAttendee = Attendee.MapFrom(m),
        AfterAttendee = Attendee.MapFrom(id, i)
    };

    public static UpdateAttendeePayload MapFrom(AttendeeModel m, IEnumerable<Error> errors) => new()
    {
        BeforeAttendee = Attendee.MapFrom(m),
        AttendeeErrors = errors
    };

    public static UpdateAttendeePayload MapFrom(Error error) => new()
    {
        AttendeeErrors = [error]
    };
}
