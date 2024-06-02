namespace GraphQL.Schemas.Database.Tracks;

using GraphQL.DataLoaders.Database;
using GraphQL.Repositories.Database.Sessions;
using GraphQL.Repositories.Database.Tracks;

[Node]
[GraphQLDescription("A track resource.")]
public class Track
{
    [ID]
    [GraphQLDescription("The unique identifier of this track.")]
    public int Id { get; set; } = default!;

    [GraphQLDescription("The name of this track.")]
    public string Name { get; set; } = default!;

    [GraphQLDescription("A list of session resources associated to this track.")]
    public IEnumerable<SessionModel> Sessions { get; set; } = [];

    [NodeResolver]
    public static Task<Track> GetNodeAsync(
        [ID(nameof(Track))] int id,
        [Service] TrackByIdBatchDataLoader dataLoader,
        CancellationToken ctx) => dataLoader.LoadAsync(id, ctx);

    public static Track MapFrom(TrackModel r) => new()
    {
        Id = r.Id,
        Name = r.Name
    };
}
