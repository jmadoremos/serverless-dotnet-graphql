namespace GraphQL.Database.Schemas.Speakers;

using GraphQL.Database.Repositories.Speakers;
using System.Linq;

[ExtendObjectType("Query")]
public class SpeakerQuery([Service] ISpeakerRepository speakers)
{
    [UsePaging]
    [UseProjection]
    [UseFiltering]
    [UseSorting]
    [GraphQLDescription("A list of speakers from all sessions.")]
    public async Task<IEnumerable<Speaker>> GetSpeakersAsync(CancellationToken ctx)
    {
        var result = await speakers.GetAllSpeakersAsync(ctx);
        return result.Select(Speaker.MapFrom);
    }

    [GraphQLDescription("A speaker of any session.")]
    public async Task<Speaker?> GetSpeakerAsync(
        [ID(nameof(Speaker))] int id,
        CancellationToken ctx)
    {
        var result = await speakers.GetSpeakerByIdAsync(id, ctx);

        if (result is null)
        {
            return null;
        }

        return Speaker.MapFrom(result);
    }
}
