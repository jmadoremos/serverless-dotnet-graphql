namespace GraphQL.Exceptions.Database;

using GraphQL.Repositories.Database.Sessions;

public class SessionTitleTakenException : GraphQLException
{
    public SessionTitleTakenException() : base("Session title is already taken") =>
        this.Attributes =
        [
            "input",
            nameof(SessionInput.Title)
        ];
}
