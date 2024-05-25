namespace GraphQL.Repositories.StarWars.Characters;

public interface ICharacterRepository
{
    Task<SwapiResponseList<CharacterApiResponse>> GetAllAsync(CancellationToken ctx);

    Task<CharacterApiResponse?> GetByIdAsync(int id, CancellationToken ctx);
}
