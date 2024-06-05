namespace GraphQL.Database.Schemas.Sessions;

using GraphQL.Database.Schemas.Speakers;

[GraphQLDescription("The input of add or remove session speaker mapping.")]
public class AddRemoveSessionSpeaker
{
    [ID(nameof(Session))]
    [GraphQLDescription("The identifier of this session.")]
    public int SessionId { get; set; } = default!;

    [ID(nameof(Speaker))]
    [GraphQLDescription("The identifier of this speaker.")]
    public int SpeakerId { get; set; } = default!;
}
