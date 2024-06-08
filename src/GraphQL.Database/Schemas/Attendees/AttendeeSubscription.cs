namespace GraphQL.Database.Schemas.Attendees;

[ExtendObjectType("Subscription")]
public class AttendeeSubscription
{
    [Subscribe]
    public AddRemoveAttendeePayload AttendeeAdded([EventMessage] AddRemoveAttendeePayload payload) =>
        payload;

    [Subscribe]
    public UpdateAttendeePayload AttendeeUpdated([EventMessage] UpdateAttendeePayload payload) =>
        payload;

    [Subscribe]
    public AddRemoveAttendeePayload AttendeeRemoved([EventMessage] AddRemoveAttendeePayload payload) =>
        payload;
}
