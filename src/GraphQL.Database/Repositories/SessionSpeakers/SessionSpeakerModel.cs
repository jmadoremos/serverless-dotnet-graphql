namespace GraphQL.Database.Repositories.SessionSpeakers;

using GraphQL.Database.Repositories.Sessions;
using GraphQL.Database.Repositories.Speakers;
using System.ComponentModel.DataAnnotations.Schema;

[Table("SessionSpeakerMapping")]
public class SessionSpeakerModel : SessionSpeakerModelInput
{
    public SessionModel Session { get; set; } = default!;

    public SpeakerModel Speaker { get; set; } = default!;

    public static SessionSpeakerModel MapFrom(SessionSpeakerModelInput i) => new()
    {
        SessionId = i.SessionId,
        SpeakerId = i.SpeakerId
    };
}
