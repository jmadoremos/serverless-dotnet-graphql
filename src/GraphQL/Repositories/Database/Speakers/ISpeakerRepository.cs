namespace GraphQL.Repositories.Database.Speakers;

public interface ISpeakerRepository
{
    Task<IQueryable<Speaker>> GetAllAsync(CancellationToken ctx);

    Task<Speaker> GetByIdAsync(int id, CancellationToken ctx);

    Task<int> CreateAsync(SpeakerInput input, CancellationToken ctx);

    Task UpdateAsync(int id, SpeakerInput input, CancellationToken ctx);

    Task DeleteAsync(int id, CancellationToken ctx);
}
