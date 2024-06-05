namespace GraphQL.Database.Schemas.Speakers;

using ErrorOr;
using GraphQL.Database.Repositories.Speakers;

public class UpdateSpeakerPayload
{
    [GraphQLDescription("The speaker resource before the changes.")]
    public Speaker? BeforeSpeaker { get; set; } = default!;

    [GraphQLDescription("The speaker resource after the changes.")]
    public Speaker? AfterSpeaker { get; set; } = default!;

    [GraphQLDescription("A list of errors encountered.")]
    public IEnumerable<Error> SpeakerErrors { get; set; } = [];

    public static UpdateSpeakerPayload MapFrom(int id, SpeakerModel m, SpeakerModelInput i) => new()
    {
        BeforeSpeaker = Speaker.MapFrom(m),
        AfterSpeaker = Speaker.MapFrom(id, i)
    };

    public static UpdateSpeakerPayload MapFrom(SpeakerModel m, IEnumerable<Error> errors) => new()
    {
        BeforeSpeaker = Speaker.MapFrom(m),
        SpeakerErrors = errors
    };

    public static UpdateSpeakerPayload MapFrom(Error error) => new()
    {
        SpeakerErrors = [error]
    };
}
