namespace GraphQL.Schemas.Database.Tracks;

using GraphQL.Repositories.Database.Sessions;
using GraphQL.Repositories.Database.Tracks;

[GraphQLDescription("A track resource.")]
public class TrackSchema
{
    [GraphQLType(typeof(IdType))]
    public int Id { get; set; } = default!;

    [GraphQLDescription("The name of this track.")]
    public string Name { get; set; } = default!;

    [GraphQLDescription("A list of session resources associated to this track.")]
    public IEnumerable<Session> Sessions { get; set; } = [];

    public static TrackSchema MapFrom(Track r) => new()
    {
        Id = r.Id,
        Name = r.Name
    };
}
