namespace GraphQL.Schemas.Database.Sessions;

[GraphQLDescription("An attendee resource of sessions.")]
public class AddRemoveSessionAttendee
{
    [GraphQLDescription("The identifier of this session.")]
    public int SessionId { get; set; } = default!;

    [GraphQLDescription("The identifier of this attendee.")]
    public int AttendeeId { get; set; } = default!;
}
