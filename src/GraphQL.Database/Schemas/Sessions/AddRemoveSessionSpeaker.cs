namespace GraphQL.Database.Schemas.Sessions;

[GraphQLDescription("A speaker resource of sessions.")]
public class AddRemoveSessionSpeaker
{
    [GraphQLDescription("The identifier of this session.")]
    public int SessionId { get; set; } = default!;

    [GraphQLDescription("The identifier of this speaker.")]
    public int SpeakerId { get; set; } = default!;
}
