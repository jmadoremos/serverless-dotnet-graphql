namespace GraphQL.Schemas.StarWars.People;

using GraphQL.Extensions;
using GraphQL.Repositories.StarWars.Films;
using GraphQL.Repositories.StarWars.People;
using GraphQL.Repositories.StarWars.Planets;
using GraphQL.Schemas.StarWars.Films;
using GraphQL.Schemas.StarWars.Planets;
using System.Collections.Generic;

[ExtendObjectType("Query")]
public class PersonQuery(
    [Service] IFilmRepository films,
    [Service] IPersonRepository people,
    [Service] IPlanetRepository planets)
{
    public async Task<IEnumerable<PersonSchema>?> GetPersonsAsync(CancellationToken ctx)
    {
        // Call API to retrieve all data
        var response = await people.GetAllAsync(ctx);

        // Return null if no result found
        if (response.Count == 0)
        {
            return null;
        }

        // Map to schema and queue object type resolution
        var queue = response.Results.Select(s => this.MapToSchemaAsync(s, ctx));

        // Resolve
        return await Task.WhenAll(queue);
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
        return await this.MapToSchemaAsync(response, ctx);
    }

    private async Task<PersonSchema> MapToSchemaAsync(
        PersonApiResponse response,
        CancellationToken ctx)
    {
        var person = PersonSchema.MapFrom(response);

        var homeplanet = await planets.GetByIdAsync(response.Homeworld.ExtractSwapiId(), ctx);

        var filmQueue = response.Films.Select(a => films.GetByIdAsync(a.ExtractSwapiId(), ctx));
        var filmResponses = await Task.WhenAll(filmQueue);

        person.Homeworld = homeplanet is not null
            ? PlanetSchema.MapFrom(homeplanet)
            : null;

#pragma warning disable CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).
        person.Films = filmResponses is not null && !filmResponses.IsAnyNull()
            ? filmResponses.Select(FilmSchema.MapFrom)
            : null;
#pragma warning restore CS8622 // Nullability of reference types in type of parameter doesn't match the target delegate (possibly because of nullability attributes).

        return person;
    }
}
