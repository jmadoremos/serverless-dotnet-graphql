namespace GraphQL.Database.Schemas.Sessions;

using GraphQL.Database.Schemas.Attendees;

[GraphQLDescription("An attendee resource of sessions.")]
public class AddRemoveSessionAttendee
{
    [ID(nameof(Session))]
    [GraphQLDescription("The identifier of this session.")]
    public int SessionId { get; set; } = default!;

    [ID(nameof(Attendee))]
    [GraphQLDescription("The identifier of this attendee.")]
    public int AttendeeId { get; set; } = default!;
}
