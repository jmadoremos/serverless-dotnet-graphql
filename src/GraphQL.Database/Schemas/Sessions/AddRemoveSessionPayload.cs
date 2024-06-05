namespace GraphQL.Database.Schemas.Sessions;

using ErrorOr;
using GraphQL.Database.Repositories.Sessions;

[GraphQLDescription("A result of add or remove session resource.")]
public class AddRemoveSessionPayload
{
    [GraphQLDescription("The session resource added or removed.")]
    public Session? Session { get; set; } = default!;

    [GraphQLDescription("A list of errors encountered.")]
    public IEnumerable<Error> SessionErrors { get; set; } = [];

    public static AddRemoveSessionPayload MapFrom(int id, SessionModelInput i) => new()
    {
        Session = Session.MapFrom(id, i)
    };

    public static AddRemoveSessionPayload MapFrom(SessionModel m) => new()
    {
        Session = Session.MapFrom(m)
    };

    public static AddRemoveSessionPayload MapFrom(IEnumerable<Error> errors) => new()
    {
        SessionErrors = errors
    };

    public static AddRemoveSessionPayload MapFrom(Error error) => new()
    {
        SessionErrors = [error]
    };
}
