namespace GraphQL.Database.Errors;

using ErrorOr;
using GraphQL.Database.Repositories.SessionAttendees;

public static class SessionAttendeeError
{
    public static Error AlreadyExists(
        int sessionId,
        int attendeeId
    ) => Error.Validation(
            code: "SESSION_ATTENDEE_EXISTS",
                description: "Session attendee mapping already exists.",
            metadata: new Dictionary<string, object>
            {
                { nameof(SessionAttendeeModel.SessionId), sessionId },
                { nameof(SessionAttendeeModel.AttendeeId), attendeeId }
            }
        );

    public static Error NotFound(
        int sessionId,
        int attendeeId
    ) => Error.Validation(
            code: "SESSION_ATTENDEE_NOT_FOUND",
                description: "Session attendee mapping not found.",
            metadata: new Dictionary<string, object>
            {
                { nameof(SessionAttendeeModel.SessionId), sessionId },
                { nameof(SessionAttendeeModel.AttendeeId), attendeeId }
            }
        );
}
