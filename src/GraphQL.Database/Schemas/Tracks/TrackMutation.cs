namespace GraphQL.Database.Schemas.Tracks;

using GraphQL.Database.Errors;
using GraphQL.Database.Repositories.Tracks;
using HotChocolate.Subscriptions;

[ExtendObjectType("Mutation")]
public class TrackMutation(
    [Service] ITopicEventSender sender,
    [Service] ITrackRepository tracks)
{
    [GraphQLDescription("Adds a track resource.")]
    public async Task<AddRemoveTrackPayload> AddTrackAsync(
        AddTrackInput input,
        CancellationToken ctx)
    {
        var track = TrackModelInput.MapFrom(input);

        var id = await tracks.CreateTrackAsync(track, ctx);

        if (id.IsError)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(TrackSubscription.TrackAdded),
                AddRemoveTrackPayload.MapFrom(id.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(TrackSubscription.TrackAdded),
            AddRemoveTrackPayload.MapFrom(id.Value, track),
            ctx);
    }

    [GraphQLDescription("Updates a track resource.")]
    public async Task<UpdateTrackPayload> UpdateTrackAsync(
        [ID(nameof(Track))] int id,
        UpdateTrackInput input,
        CancellationToken ctx)
    {
        var entity = await tracks.GetTrackByIdAsync(id, ctx);

        if (entity is null)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(TrackSubscription.TrackUpdated),
                UpdateTrackPayload.MapFrom(TrackError.NotFound(id)),
                ctx);
        }

        var track = TrackModelInput.MapFrom(entity, input);

        var update = await tracks.UpdateTrackAsync(id, track, ctx);

        if (update.IsError)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(TrackSubscription.TrackUpdated),
                UpdateTrackPayload.MapFrom(entity, update.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(TrackSubscription.TrackUpdated),
            UpdateTrackPayload.MapFrom(id, entity, track),
            ctx);
    }

    [GraphQLDescription("Removes a track resource.")]
    public async Task<AddRemoveTrackPayload> RemoveTrackAsync(
        [ID(nameof(Track))] int id,
        CancellationToken ctx)
    {
        var entity = await tracks.GetTrackByIdAsync(id, ctx);

        if (entity is null)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(TrackSubscription.TrackRemoved),
                AddRemoveTrackPayload.MapFrom(AttendeeError.NotFound(id)),
                ctx);
        }

        var delete = await tracks.DeleteTrackAsync(id, ctx);

        if (delete.IsError)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(TrackSubscription.TrackRemoved),
                AddRemoveTrackPayload.MapFrom(delete.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(TrackSubscription.TrackRemoved),
            AddRemoveTrackPayload.MapFrom(entity),
            ctx);
    }

    private async Task<TPayload> PublishAndReturnPayloadAsync<TPayload>(
        string eventName,
        TPayload payload,
        CancellationToken ctx)
    {
        await sender.SendAsync(eventName, payload, ctx);

        return payload;
    }
}
