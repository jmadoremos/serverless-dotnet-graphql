namespace GraphQL.Database.Schemas.Speakers;

using ErrorOr;
using System.Collections.Generic;
using GraphQL.Database.Repositories.Speakers;

[GraphQLDescription("A result of add or remove speaker resource.")]
public class AddRemoveSpeakerPayload
{
    [GraphQLDescription("The speaker resource added or removed.")]
    public Speaker? Speaker { get; set; } = default!;

    [GraphQLDescription("A list of errors encountered.")]
    public IEnumerable<Error> SpeakerErrors { get; set; } = [];

    public static AddRemoveSpeakerPayload MapFrom(int id, SpeakerModelInput i) => new()
    {
        Speaker = Speaker.MapFrom(id, i)
    };

    public static AddRemoveSpeakerPayload MapFrom(SpeakerModel m) => new()
    {
        Speaker = Speaker.MapFrom(m)
    };

    public static AddRemoveSpeakerPayload MapFrom(IEnumerable<Error> errors) => new()
    {
        SpeakerErrors = errors
    };

    public static AddRemoveSpeakerPayload MapFrom(Error error) => new()
    {
        SpeakerErrors = [error]
    };
}
