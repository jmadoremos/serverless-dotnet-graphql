namespace GraphQL.Database.Schemas.Sessions;

using ErrorOr;

[GraphQLDescription("A result of add or remove session attendee mapping.")]
public class AddRemoveSessionAttendeePayload
{
    [GraphQLDescription("A list of errors encountered.")]
    public IEnumerable<Error> SessionAttendeeErrors { get; set; } = [];

    public static AddRemoveSessionAttendeePayload MapFrom(IEnumerable<Error> errors) => new()
    {
        SessionAttendeeErrors = errors
    };
}
