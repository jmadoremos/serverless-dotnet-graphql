namespace GraphQL.Exceptions.Database;

using GraphQL.Repositories.Database.Attendees;

public class UsernameTakenException : GraphQLException
{
    public string Suggestion { get; set; } = default!;

    public UsernameTakenException() : base("Username is already taken") =>
        this.Attributes =
        [
            "input",
            nameof(AttendeeInput.UserName)
        ];
}
