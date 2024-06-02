namespace GraphQL.Schemas.Database.Speakers;

using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.Speakers;

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
        [GraphQLType(typeof(IdType))] int id,
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
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        var entity = await speakers.GetSpeakerByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(SpeakerModel.Id));

        await speakers.DeleteSpeakerAsync(id, ctx);

        return Speaker.MapFrom(entity);
    }
}
