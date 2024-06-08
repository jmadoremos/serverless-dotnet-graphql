namespace GraphQL.Database.Schemas.Tracks;

[ExtendObjectType("Subscription")]
public class TrackSubscription
{
    [Subscribe]
    public AddRemoveTrackPayload TrackAdded([EventMessage] AddRemoveTrackPayload payload) =>
        payload;

    [Subscribe]
    public UpdateTrackPayload TrackUpdated([EventMessage] UpdateTrackPayload payload) =>
        payload;

    [Subscribe]
    public AddRemoveTrackPayload TrackRemoved([EventMessage] AddRemoveTrackPayload payload) =>
        payload;
}
