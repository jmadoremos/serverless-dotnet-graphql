namespace GraphQL.Schemas.Database.Speakers;

using GraphQL.Exceptions.Database;
using GraphQL.Repositories.Database.Speakers;
using System.Linq;

[ExtendObjectType("Query")]
public class SpeakerQuery([Service] ISpeakerRepository speakers)
{
    [UsePaging]
    [GraphQLDescription("A list of speakers from all sessions.")]
    public async Task<IEnumerable<Speaker>> GetSpeakersAsync(CancellationToken ctx)
    {
        var result = await speakers.GetAllSpeakersAsync(ctx);
        return result.Select(Speaker.MapFrom);
    }

    [GraphQLDescription("A speaker of any session.")]
    public async Task<Speaker> GetSpeakerAsync(
        [ID(nameof(Speaker))] int id,
        CancellationToken ctx)
    {
        var result = await speakers.GetSpeakerByIdAsync(id, ctx)
            ?? throw new UserNotFoundException(nameof(Speaker.Id));

        return Speaker.MapFrom(result);
    }
}
