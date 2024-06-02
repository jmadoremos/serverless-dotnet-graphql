namespace GraphQL.Exceptions.Database;

using GraphQL.Repositories.Database.Tracks;

public class TrackNameTakenException : GraphQLException
{
    public TrackNameTakenException() : base("Track name is already taken") =>
        this.Attributes =
        [
            "input",
            nameof(TrackModelInput.Name)
        ];
}
