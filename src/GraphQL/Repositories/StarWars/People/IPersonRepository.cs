namespace GraphQL.Repositories.StarWars.People;

public interface IPersonRepository
{
    Task<SwapiResponseList<PersonApiResponse>> GetAllAsync(CancellationToken ctx);

    Task<PersonApiResponse?> GetByIdAsync(int id, CancellationToken ctx);
}
