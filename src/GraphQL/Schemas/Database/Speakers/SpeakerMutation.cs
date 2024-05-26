namespace GraphQL.Schemas.Database.Speakers;

using GraphQL.Repositories.Database.Speakers;

[ExtendObjectType("Mutation")]
public class SpeakerMutation([Service] ISpeakerRepository speakers)
{
    [GraphQLDescription("Adds a speaker resource.")]
    public async Task<SpeakerSchema> AddSpeakerAsync(
        AddSpeakerSchema input,
        CancellationToken ctx)
    {
        var attendee = SpeakerInput.MapFrom(input);

        var id = await speakers.CreateAsync(attendee, ctx);

        var entity = await speakers.GetByIdAsync(id, ctx);

        return SpeakerSchema.MapFrom(entity);
    }

    [GraphQLDescription("Updates a speaker resource.")]
    public async Task<SpeakerSchema> UpdateSpeakerAsync(
        [GraphQLType(typeof(IdType))] int id,
        UpdateSpeakerSchema input,
        CancellationToken ctx)
    {
        var entity = await speakers.GetByIdAsync(id, ctx);

        var attendee = SpeakerInput.MapFrom(entity, input);

        await speakers.UpdateAsync(id, attendee, ctx);

        entity = await speakers.GetByIdAsync(id, ctx);

        return SpeakerSchema.MapFrom(entity);
    }

    [GraphQLDescription("Deletes a speaker resource.")]
    public async Task<SpeakerSchema> DeleteSpeakerAsync(
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        var entity = await speakers.GetByIdAsync(id, ctx);

        await speakers.DeleteAsync(id, ctx);

        return SpeakerSchema.MapFrom(entity);
    }
}
