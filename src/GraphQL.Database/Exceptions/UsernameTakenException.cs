namespace GraphQL.Database.Exceptions;

public class UsernameTakenException : GraphQLException
{
    public string Suggestion { get; set; } = default!;

    public UsernameTakenException(string attribute) : base("User name is already taken") =>
        this.Attributes =
        [
            "input",
            attribute
        ];
}
