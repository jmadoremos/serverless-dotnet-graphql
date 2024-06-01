namespace GraphQL.Schemas.Database.Speakers;

using GraphQL.Repositories.Database.Speakers;
using GraphQL.Schemas.Database.Sessions;

[GraphQLDescription("A speaker resource of sessions.")]
public class SpeakerSchema
{
    [GraphQLType(typeof(IdType))]
    public int Id { get; set; } = default!;

    [GraphQLDescription("The name of this speaker.")]
    public string Name { get; set; } = default!;

    [GraphQLDescription("The biography of this speaker.")]
    public string? Bio { get; set; } = default!;

    [GraphQLDescription("The website of this speaker.")]
    public string? Website { get; set; } = default!;

    [GraphQLDescription("The sessions where this speaker will be speaking, is speaking, or has spoken at.")]
    public IEnumerable<SessionSchema> Sessions { get; set; } = [];

    public static SpeakerSchema MapFrom(Speaker r) => new()
    {
        Id = r.Id,
        Name = r.Name,
        Bio = r.Bio,
        Website = r.WebSite
    };
}
