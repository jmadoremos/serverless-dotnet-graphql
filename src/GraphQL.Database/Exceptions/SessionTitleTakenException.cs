namespace GraphQL.Database.Exceptions;

using GraphQL.Database.Repositories.Sessions;

public class SessionTitleTakenException : GraphQLException
{
    public SessionTitleTakenException() : base("Session title is already taken") =>
        this.Attributes =
        [
            "input",
            nameof(SessionModelInput.Title)
        ];
}
