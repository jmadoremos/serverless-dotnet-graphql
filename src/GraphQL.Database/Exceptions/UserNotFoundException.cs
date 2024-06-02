namespace GraphQL.Database.Exceptions;

public class UserNotFoundException : GraphQLException
{
    public UserNotFoundException(string attribute) : base("User not found") =>
        this.Attributes =
        [
            "input",
            attribute
        ];
}
