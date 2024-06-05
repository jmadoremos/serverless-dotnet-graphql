namespace GraphQL.Database.Schemas.Tracks;

[GraphQLDescription("The input of add track resource.")]
public class AddTrackInput
{
    [GraphQLDescription("The name of this track.")]
    public string Name { get; set; } = default!;
}
