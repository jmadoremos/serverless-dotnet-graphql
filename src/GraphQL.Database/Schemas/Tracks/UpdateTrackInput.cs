namespace GraphQL.Database.Schemas.Tracks;

[GraphQLDescription("A track resource.")]
public class UpdateTrackInput
{
    [GraphQLDescription("The name of this track.")]
    public string? Name { get; set; } = default!;
}
