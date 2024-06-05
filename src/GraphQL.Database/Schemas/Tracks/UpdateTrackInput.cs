namespace GraphQL.Database.Schemas.Tracks;

[GraphQLDescription("The input of update track resource.")]

public class UpdateTrackInput
{
    [GraphQLDescription("The name of this track.")]
    public string? Name { get; set; } = default!;
}
