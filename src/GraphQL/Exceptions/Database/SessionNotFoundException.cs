namespace GraphQL.Exceptions.Database;

using GraphQL.Repositories.Database.Sessions;

public class SessionNotFoundException : GraphQLException
{
    public SessionNotFoundException() : base("Session not found") =>
        this.Attributes =
        [
            "input",
            nameof(SessionModel.Title)
        ];
}
