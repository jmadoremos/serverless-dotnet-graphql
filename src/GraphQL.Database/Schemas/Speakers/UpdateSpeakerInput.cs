namespace GraphQL.Database.Schemas.Speakers;

[GraphQLDescription("A speaker resource of sessions.")]
public class UpdateSpeakerInput
{
    [GraphQLDescription("The name of this speaker.")]
    public string? Name { get; set; } = default!;

    [GraphQLDescription("The biography of this speaker.")]
    public string? Bio { get; set; } = default!;

    [GraphQLDescription("The website of this speaker.")]
    public string? Website { get; set; } = default!;
}