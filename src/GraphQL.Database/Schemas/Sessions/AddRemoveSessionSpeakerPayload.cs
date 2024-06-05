namespace GraphQL.Database.Schemas.Sessions;

using ErrorOr;

public class AddRemoveSessionSpeakerPayload
{
    [GraphQLDescription("A list of errors encountered.")]
    public IEnumerable<Error> SessionSpeakerErrors { get; set; } = [];

    public static AddRemoveSessionSpeakerPayload MapFrom(IEnumerable<Error> errors) => new()
    {
        SessionSpeakerErrors = errors
    };
}
