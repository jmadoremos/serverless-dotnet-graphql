namespace GraphQL.Database.Schemas.Speakers;

using GraphQL.Database.DataLoaders;
using GraphQL.Database.Repositories.Speakers;
using GraphQL.Database.Schemas.Sessions;

[Node]
[GraphQLDescription("A speaker resource of sessions.")]
public class Speaker
{
    [ID]
    [GraphQLDescription("The unique identifier of this speaker.")]
    public int Id { get; set; } = default!;

    [GraphQLDescription("The name of this speaker.")]
    public string Name { get; set; } = default!;

    [GraphQLDescription("The biography of this speaker.")]
    public string? Bio { get; set; } = default!;

    [GraphQLDescription("The website of this speaker.")]
    public string? Website { get; set; } = default!;

    [GraphQLDescription("The sessions where this speaker will be speaking, is speaking, or has spoken at.")]
    public IEnumerable<Session> Sessions { get; set; } = [];

    [NodeResolver]
    public static Task<Speaker> GetNodeAsync(
        [ID(nameof(Speaker))] int id,
        [Service] SpeakerByIdBatchDataLoader dataLoader,
        CancellationToken ctx) => dataLoader.LoadAsync(id, ctx);

    public static Speaker MapFrom(SpeakerModel r) => new()
    {
        Id = r.Id,
        Name = r.Name,
        Bio = r.Bio,
        Website = r.WebSite
    };
}
