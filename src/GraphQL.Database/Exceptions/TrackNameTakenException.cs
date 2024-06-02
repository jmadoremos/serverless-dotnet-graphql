namespace GraphQL.Database.Exceptions;

using GraphQL.Database.Repositories.Tracks;

public class TrackNameTakenException : GraphQLException
{
    public TrackNameTakenException() : base("Track name is already taken") =>
        this.Attributes =
        [
            "input",
            nameof(TrackModelInput.Name)
        ];
}
