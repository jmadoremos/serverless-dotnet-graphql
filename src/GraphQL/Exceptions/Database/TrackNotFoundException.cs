namespace GraphQL.Exceptions.Database;

using GraphQL.Repositories.Database.Tracks;

public class TrackNotFoundException : GraphQLException
{
    public TrackNotFoundException() : base("Track not found") =>
        this.Attributes =
        [
            "input",
            nameof(Track.Name)
        ];
}
