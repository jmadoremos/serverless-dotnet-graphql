namespace GraphQL.Database.Exceptions;

using GraphQL.Database.Repositories.Sessions;

public class SessionNotFoundException : GraphQLException
{
    public SessionNotFoundException() : base("Session not found") =>
        this.Attributes =
        [
            "input",
            nameof(SessionModel.Title)
        ];
}
