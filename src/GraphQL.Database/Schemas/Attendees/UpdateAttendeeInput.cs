namespace GraphQL.Database.Schemas.Attendees;

[GraphQLDescription("An attendee resource of sessions.")]
public class UpdateAttendeeInput
{
    [GraphQLDescription("The firstnames of this attendee.")]
    public string? Firstname { get; set; } = default!;

    [GraphQLDescription("The lastnames of this attendee.")]
    public string? Lastname { get; set; } = default!;

    [GraphQLDescription("The email address of this attendee.")]
    public string? Email { get; set; } = default!;
}
