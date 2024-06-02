namespace GraphQL.WebAPI.Schemas.StarWars;

[ExtendObjectType("Query")]
public class StarWarsQuery()
{
    [GraphQLDescription("The base URL of the Star Wars API.")]
    public StarWars GetStarWars() => new()
    {
        BaseUrl = new Uri("https://swapi.dev/api/")
    };
}
