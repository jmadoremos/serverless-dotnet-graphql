namespace GraphQL.Database.Errors;

using ErrorOr;
using GraphQL.Database.Repositories.SessionSpeakers;

public static class SessionSpeakerError
{
    public static Error AlreadyExists(
        int sessionId,
        int speakerId
    ) => Error.Validation(
            code: "SESSION_SPEAKER_EXISTS",
                description: "Session speaker mapping already exists.",
            metadata: new Dictionary<string, object>
            {
                { nameof(SessionSpeakerModel.SessionId), sessionId },
                { nameof(SessionSpeakerModel.SpeakerId), speakerId }
            }
        );

    public static Error NotFound(
        int sessionId,
        int speakerId
    ) => Error.Validation(
            code: "SESSION_SPEAKER_NOT_FOUND",
            description: "Session speaker mapping not found.",
            metadata: new Dictionary<string, object>
            {
                { nameof(SessionSpeakerModel.SessionId), sessionId },
                { nameof(SessionSpeakerModel.SpeakerId), speakerId }
            }
        );
}
