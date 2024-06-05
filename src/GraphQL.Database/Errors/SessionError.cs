namespace GraphQL.Database.Errors;

using ErrorOr;
using GraphQL.Database.Repositories.Sessions;

public static class SessionError
{
    public static Error NotFound(int id) =>
        Error.NotFound(
            code: "SESSION_ID_NOT_FOUND",
            description: $"Session with {nameof(SessionModel.Id)} not found.",
            metadata: new Dictionary<string, object>
            {
                { nameof(SessionModel.Id), id }
            }
        );

    public static Error TitleTaken(string title) =>
        Error.Validation(
            code: "SESSION_TITLE_TAKEN",
            description: $"Session {nameof(SessionModel.Title)} is already taken.",
            metadata: new Dictionary<string, object>
            {
                { nameof(SessionModel.Title), title }
            }
        );
}
