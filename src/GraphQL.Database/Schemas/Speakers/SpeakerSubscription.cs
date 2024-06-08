namespace GraphQL.Database.Schemas.Speakers;

[ExtendObjectType("Subscription")]
public class SpeakerSubscription
{
    [Subscribe]
    public AddRemoveSpeakerPayload SpeakerAdded([EventMessage] AddRemoveSpeakerPayload payload) =>
        payload;

    [Subscribe]
    public UpdateSpeakerPayload SpeakerUpdated([EventMessage] UpdateSpeakerPayload payload) =>
        payload;

    [Subscribe]
    public AddRemoveSpeakerPayload SpeakerRemoved([EventMessage] AddRemoveSpeakerPayload payload) =>
        payload;
}
