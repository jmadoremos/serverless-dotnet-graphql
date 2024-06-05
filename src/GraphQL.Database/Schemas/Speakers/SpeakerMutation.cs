namespace GraphQL.Database.Schemas.Speakers;

using GraphQL.Database.Errors;
using GraphQL.Database.Repositories.Speakers;

[ExtendObjectType("Mutation")]
public class SpeakerMutation([Service] ISpeakerRepository speakers)
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
            return AddRemoveSpeakerPayload.MapFrom(id.Errors);
        }

        return AddRemoveSpeakerPayload.MapFrom(id.Value, speaker);
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
            return UpdateSpeakerPayload.MapFrom(SpeakerError.NotFound(id));
        }

        var attendee = SpeakerModelInput.MapFrom(entity, input);

        var update = await speakers.UpdateSpeakerAsync(id, attendee, ctx);

        if (update.IsError)
        {
            return UpdateSpeakerPayload.MapFrom(entity, update.Errors);
        }

        return UpdateSpeakerPayload.MapFrom(id, entity, attendee);
    }

    [GraphQLDescription("Removes a speaker resource.")]
    public async Task<AddRemoveSpeakerPayload> RemoveSpeakerAsync(
        [ID(nameof(Speaker))] int id,
        CancellationToken ctx)
    {
        var entity = await speakers.GetSpeakerByIdAsync(id, ctx);

        if (entity is null)
        {
            return AddRemoveSpeakerPayload.MapFrom(SpeakerError.NotFound(id));
        }

        var delete = await speakers.DeleteSpeakerAsync(id, ctx);

        if (delete.IsError)
        {
            return AddRemoveSpeakerPayload.MapFrom(delete.Errors);
        }

        return AddRemoveSpeakerPayload.MapFrom(entity);
    }
}
