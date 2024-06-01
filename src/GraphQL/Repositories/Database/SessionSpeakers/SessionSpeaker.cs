namespace GraphQL.Repositories.Database.SessionSpeakers;

using System.ComponentModel.DataAnnotations.Schema;
using GraphQL.Repositories.Database.Sessions;
using GraphQL.Repositories.Database.Speakers;

[Table("SessionSpeakerMapping")]
public class SessionSpeaker : SessionSpeakerInput
{
    public Session Session { get; set; } = default!;

    public Speaker Speaker { get; set; } = default!;

    public static SessionSpeaker MapFrom(SessionSpeakerInput i) => new()
    {
        SessionId = i.SessionId,
        SpeakerId = i.SpeakerId
    };
}
