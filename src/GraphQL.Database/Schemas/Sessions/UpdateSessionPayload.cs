namespace GraphQL.Database.Schemas.Sessions;

using ErrorOr;
using GraphQL.Database.Repositories.Sessions;

public class UpdateSessionPayload
{
    [GraphQLDescription("The session resource before the changes.")]
    public Session? BeforeSession { get; set; } = default!;

    [GraphQLDescription("The session resource after the changes.")]
    public Session? AfterSession { get; set; } = default!;

    [GraphQLDescription("A list of errors encountered.")]
    public IEnumerable<Error> SessionErrors { get; set; } = [];

    public static UpdateSessionPayload MapFrom(int id, SessionModel m, SessionModelInput i) => new()
    {
        BeforeSession = Session.MapFrom(m),
        AfterSession = Session.MapFrom(id, i)
    };

    public static UpdateSessionPayload MapFrom(SessionModel m, IEnumerable<Error> errors) => new()
    {
        BeforeSession = Session.MapFrom(m),
        SessionErrors = errors
    };

    public static UpdateSessionPayload MapFrom(Error error) => new()
    {
        SessionErrors = [error]
    };
}
