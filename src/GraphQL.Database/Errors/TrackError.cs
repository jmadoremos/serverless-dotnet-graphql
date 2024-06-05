namespace GraphQL.Database.Errors;

using ErrorOr;
using GraphQL.Database.Repositories.Tracks;

public static class TrackError
{
    public static Error NameTaken(string name) =>
        Error.Validation(
            code: "TRACK_NAME_TAKEN",
            description: $"Track {nameof(TrackModel.Name)} is already taken.",
            metadata: new Dictionary<string, object>
            {
                { nameof(TrackModel.Name), name }
            }
        );

    public static Error NotFound(int id) =>
        Error.NotFound(
            code: "TRACK_ID_NOT_FOUND",
            description: $"Track with {nameof(TrackModel.Id)} not found.",
            metadata: new Dictionary<string, object>
            {
                { nameof(TrackModel.Id), id }
            }
        );
}
