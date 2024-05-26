namespace GraphQL.Schemas.Database.Speakers;

using GraphQL.Repositories.Database.Speakers;
using System.Linq;

[ExtendObjectType("Query")]
public class SpeakerQuery([Service] ISpeakerRepository speakers)
{
    [GraphQLDescription("A list of speakers from all sessions.")]
    public async Task<IEnumerable<SpeakerSchema>> GetSpeakersAsync(CancellationToken ctx)
    {
        var result = await speakers.GetAllAsync(ctx);
        return result.Select(SpeakerSchema.MapFrom);
    }

    [GraphQLDescription("A speaker of any session.")]
    public async Task<SpeakerSchema> GetSpeakerAsync(
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        var result = await speakers.GetByIdAsync(id, ctx);
        return SpeakerSchema.MapFrom(result);
    }
}
