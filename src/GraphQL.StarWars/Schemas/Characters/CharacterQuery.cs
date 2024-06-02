namespace GraphQL.StarWars.Schemas.Characters;

using GraphQL.StarWars.Repositories;
using GraphQL.StarWars.Repositories.Responses;

[ExtendObjectType("Query")]
public class CharacterQuery(
    [Service] IStarWarsRepository<CharacterApiResponse> characters)
{
    [UsePaging]
    [GraphQLDescription("A list of people in Star Wars Universe.")]
    public async Task<IEnumerable<Character>> GetCharactersAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await characters.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return [];
        }

        // Map to schema and resolve
        return response.Results
            .Select(Character.MapFrom);
    }

    [GraphQLDescription("A person or character in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<Character?> GetCharacterByIdAsync(
        [ID(nameof(Character))] int id,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await characters.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return Character.MapFrom(response);
    }
}
