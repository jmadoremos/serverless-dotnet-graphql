namespace GraphQL.Schemas.StarWars.Characters;

using GraphQL.Repositories.StarWars.Characters;
using System.Collections.Generic;

[ExtendObjectType("Query")]
public class CharacterQuery([Service] ICharacterRepository people)
{
    [GraphQLDescription("A list of people in Star Wars Universe.")]
    public async Task<IEnumerable<CharacterSchema>?> GetCharactersAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await people.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return null;
        }

        // Map to schema and resolve
        return response.Results.Select(CharacterSchema.MapFrom);
    }

    [GraphQLDescription("A person or character in Star Wars Universe identified by the Star Wars API identifier.")]
    public async Task<CharacterSchema?> GetCharacterByIdAsync(
        [GraphQLType(typeof(IdType))] int id,
        CancellationToken ctx)
    {
        // Call API to retrieve a specific record
        var response = await people.GetByIdAsync(id, ctx);

        // If the response does not have a data, return the result
        if (response == null)
        {
            return null;
        }

        // Map and resolve
        return CharacterSchema.MapFrom(response);
    }
}
