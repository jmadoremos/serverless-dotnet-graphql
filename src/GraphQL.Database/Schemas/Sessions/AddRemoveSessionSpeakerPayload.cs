namespace GraphQL.Database.Schemas.Sessions;

using ErrorOr;

[GraphQLDescription("A result of add or remove session speaker mapping.")]
public class AddRemoveSessionSpeakerPayload
{
    [GraphQLDescription("A list of errors encountered.")]
    public IEnumerable<Error> SessionSpeakerErrors { get; set; } = [];

    public static AddRemoveSessionSpeakerPayload MapFrom(IEnumerable<Error> errors) => new()
    {
        SessionSpeakerErrors = errors
    };
}
