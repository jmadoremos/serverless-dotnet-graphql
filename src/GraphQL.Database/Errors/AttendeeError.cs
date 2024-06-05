namespace GraphQL.Database.Errors;

using ErrorOr;
using GraphQL.Database.Repositories.Attendees;

public static class AttendeeError
{
    public static Error NotFound(int id) =>
        Error.NotFound(
            code: "ATTENDEE_ID_NOT_FOUND",
            description: $"Attendee with {nameof(AttendeeModel.Id)} not found.",
            metadata: new Dictionary<string, object>
            {
                { nameof(AttendeeModel.Id), id }
            }
        );

    public static Error UserNameTaken(string userName) =>
        Error.Validation(
            code: "ATTENDEE_USERNAME_TAKEN",
            description: $"Attendee {nameof(AttendeeModel.UserName)} is already taken.",
            metadata: new Dictionary<string, object>
            {
                { nameof(AttendeeModel.UserName), userName }
            }
        );
}
