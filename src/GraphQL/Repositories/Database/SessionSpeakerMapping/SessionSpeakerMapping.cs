namespace GraphQL.Repositories.Database.SessionSpeakerMapping;

using GraphQL.Repositories.Database.Sessions;
using GraphQL.Repositories.Database.Speakers;

public class SessionSpeakerMapping
{
    public int SessionId { get; set; }

    public Session? Session { get; set; }

    public int SpeakerId { get; set; }

    public Speaker? Speaker { get; set; }
}
