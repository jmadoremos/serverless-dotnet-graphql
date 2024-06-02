namespace GraphQL.Database.Schemas.Speakers;

using GraphQL.Database.Exceptions;
using GraphQL.Database.Repositories.Speakers;

[ExtendObjectType("Mutation")]
public class SpeakerMutation([Service] ISpeakerRepository speakers)
{
    [GraphQLDescription("Adds a speaker resource.")]
    public async Task<Speaker> AddSpeakerAsync(
        AddSpeakerInput input,
        CancellationToken ctx)
    {
        var attendee = SpeakerModelInput.MapFrom(input);

        var id = await speakers.CreateSpeakerAsync(attendee, ctx);

        var entity = await speakers.GetSpeakerByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(SpeakerModel.Id));

        return Speaker.MapFrom(entity);
    }

    [GraphQLDescription("Updates a speaker resource.")]
    public async Task<Speaker> UpdateSpeakerAsync(
        [ID(nameof(Speaker))] int id,
        UpdateSpeakerInput input,
        CancellationToken ctx)
    {
        var entity = await speakers.GetSpeakerByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(SpeakerModel.Id));

        var attendee = SpeakerModelInput.MapFrom(entity, input);

        await speakers.UpdateSpeakerAsync(id, attendee, ctx);

        entity = await speakers.GetSpeakerByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(SpeakerModel.Id));

        return Speaker.MapFrom(entity);
    }

    [GraphQLDescription("Deletes a speaker resource.")]
    public async Task<Speaker> DeleteSpeakerAsync(
        [ID(nameof(Speaker))] int id,
        CancellationToken ctx)
    {
        var entity = await speakers.GetSpeakerByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(SpeakerModel.Id));

        await speakers.DeleteSpeakerAsync(id, ctx);

        return Speaker.MapFrom(entity);
    }
}
