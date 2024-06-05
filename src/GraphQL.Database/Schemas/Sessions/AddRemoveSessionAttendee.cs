namespace GraphQL.Database.Schemas.Sessions;

using GraphQL.Database.Schemas.Attendees;

[GraphQLDescription("The input of add or remove session attendee mapping.")]
public class AddRemoveSessionAttendee
{
    [ID(nameof(Session))]
    [GraphQLDescription("The identifier of this session.")]
    public int SessionId { get; set; } = default!;

    [ID(nameof(Attendee))]
    [GraphQLDescription("The identifier of this attendee.")]
    public int AttendeeId { get; set; } = default!;
}
