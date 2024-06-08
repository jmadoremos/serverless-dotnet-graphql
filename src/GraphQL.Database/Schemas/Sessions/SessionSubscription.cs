namespace GraphQL.Database.Schemas.Sessions;

[ExtendObjectType("Subscription")]
public class SessionSubscription
{
    [Subscribe]
    public AddRemoveSessionPayload SessionAdded([EventMessage] AddRemoveSessionPayload payload) =>
        payload;

    [Subscribe]
    public UpdateSessionPayload SessionUpdated([EventMessage] UpdateSessionPayload payload) =>
        payload;

    [Subscribe]
    public AddRemoveSessionPayload SessionRemoved([EventMessage] AddRemoveSessionPayload payload) =>
        payload;

    public AddRemoveSessionSpeakerPayload SessionSpeakerAdded([EventMessage] AddRemoveSessionSpeakerPayload payload) =>
        payload;

    public AddRemoveSessionAttendeePayload SessionAttendeeAdded([EventMessage] AddRemoveSessionAttendeePayload payload) =>
        payload;

    public AddRemoveSessionSpeakerPayload SessionSpeakerRemoved([EventMessage] AddRemoveSessionSpeakerPayload payload) =>
        payload;

    public AddRemoveSessionAttendeePayload SessionAttendeeRemoved([EventMessage] AddRemoveSessionAttendeePayload payload) =>
        payload;
}
