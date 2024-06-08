namespace GraphQL.Database.Schemas.Speakers;

using GraphQL.Database.Errors;
using GraphQL.Database.Repositories.Speakers;
using HotChocolate.Subscriptions;

[ExtendObjectType("Mutation")]
public class SpeakerMutation(
    [Service] ITopicEventSender sender,
    [Service] ISpeakerRepository speakers)
{
    [GraphQLDescription("Adds a speaker resource.")]
    public async Task<AddRemoveSpeakerPayload> AddSpeakerAsync(
        AddSpeakerInput input,
        CancellationToken ctx)
    {
        var speaker = SpeakerModelInput.MapFrom(input);

        var id = await speakers.CreateSpeakerAsync(speaker, ctx);

        if (id.IsError)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(SpeakerSubscription.SpeakerAdded),
                AddRemoveSpeakerPayload.MapFrom(id.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(SpeakerSubscription.SpeakerAdded),
            AddRemoveSpeakerPayload.MapFrom(id.Value, speaker),
            ctx);
    }

    [GraphQLDescription("Updates a speaker resource.")]
    public async Task<UpdateSpeakerPayload> UpdateSpeakerAsync(
        [ID(nameof(Speaker))] int id,
        UpdateSpeakerInput input,
        CancellationToken ctx)
    {
        var entity = await speakers.GetSpeakerByIdAsync(id, ctx);

        if (entity is null)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(SpeakerSubscription.SpeakerUpdated),
                UpdateSpeakerPayload.MapFrom(SpeakerError.NotFound(id)),
                ctx);
        }

        var attendee = SpeakerModelInput.MapFrom(entity, input);

        var update = await speakers.UpdateSpeakerAsync(id, attendee, ctx);

        if (update.IsError)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(SpeakerSubscription.SpeakerUpdated),
                UpdateSpeakerPayload.MapFrom(entity, update.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(SpeakerSubscription.SpeakerUpdated),
            UpdateSpeakerPayload.MapFrom(id, entity, attendee),
            ctx);
    }

    [GraphQLDescription("Removes a speaker resource.")]
    public async Task<AddRemoveSpeakerPayload> RemoveSpeakerAsync(
        [ID(nameof(Speaker))] int id,
        CancellationToken ctx)
    {
        var entity = await speakers.GetSpeakerByIdAsync(id, ctx);

        if (entity is null)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(SpeakerSubscription.SpeakerRemoved),
                AddRemoveSpeakerPayload.MapFrom(SpeakerError.NotFound(id)),
                ctx);
        }

        var delete = await speakers.DeleteSpeakerAsync(id, ctx);

        if (delete.IsError)
        {
            return await this.PublishAndReturnPayloadAsync(
                nameof(SpeakerSubscription.SpeakerRemoved),
                AddRemoveSpeakerPayload.MapFrom(delete.Errors),
                ctx);
        }

        return await this.PublishAndReturnPayloadAsync(
            nameof(SpeakerSubscription.SpeakerRemoved),
            AddRemoveSpeakerPayload.MapFrom(entity),
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
