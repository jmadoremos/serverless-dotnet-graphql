namespace GraphQL.Database.Errors;

using ErrorOr;
using GraphQL.Database.Repositories.Speakers;

public static class SpeakerError
{
    public static Error NameTaken(string name) =>
        Error.Validation(
            code: "SPEAKER_NAME_TAKEN",
            description: $"Speaker {nameof(SpeakerModel.Name)} is already taken.",
            metadata: new Dictionary<string, object>
            {
                { nameof(SpeakerModel.Name), name }
            }
        );

    public static Error NotFound(int id) =>
        Error.NotFound(
            code: "SPEAKER_ID_NOT_FOUND",
            description: $"Speaker with {nameof(SpeakerModel.Id)} not found.",
            metadata: new Dictionary<string, object>
            {
                { nameof(SpeakerModel.Id), id }
            }
        );
}
