namespace GraphQL.Schemas.StarWars.People;

using GraphQL.Repositories.StarWars.People;
using System.Collections.Generic;

[ExtendObjectType("Query")]
public class PersonQuery([Service] IPersonRepository people)
{
    public async Task<IEnumerable<PersonSchema>?> GetPeopleAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await people.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return null;
        }

        // Map to schema and resolve
        return response.Results.Select(PersonSchema.MapFrom);
    }

    public async Task<PersonSchema?> GetPersonAsync(
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
        return PersonSchema.MapFrom(response);
    }
}
