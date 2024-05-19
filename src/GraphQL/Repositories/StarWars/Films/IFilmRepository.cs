namespace GraphQL.Repositories.StarWars.Films;

public interface IFilmRepository
{
    Task<SwapiResponseList<FilmApiResponse>> GetAllAsync(CancellationToken ctx);

    Task<FilmApiResponse?> GetByIdAsync(int id, CancellationToken ctx);
}
