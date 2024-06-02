namespace GraphQL.Schemas.Database.Tracks;

[GraphQLDescription("A track resource.")]
public class AddTrackInput
{
    [GraphQLDescription("The name of this track.")]
    public string Name { get; set; } = default!;
}
