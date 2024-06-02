namespace GraphQL.Database.Exceptions;

using GraphQL.Database.Repositories.Tracks;

public class TrackNotFoundException : GraphQLException
{
    public TrackNotFoundException() : base("Track not found") =>
        this.Attributes =
        [
            "input",
            nameof(TrackModel.Name)
        ];
}
