namespace GraphQL.Database.Repositories.Speakers;

public interface ISpeakerRepository
{
    Task<IQueryable<SpeakerModel>> GetAllSpeakersAsync(CancellationToken ctx);

    Task<SpeakerModel?> GetSpeakerByIdAsync(int id, CancellationToken ctx);

    Task<IQueryable<SpeakerModel>> GetSpeakersByIdsAsync(IEnumerable<int> ids, CancellationToken ctx);

    Task<SpeakerModel?> GetSpeakerByNameAsync(string name, CancellationToken ctx);

    Task<int> CreateSpeakerAsync(SpeakerModelInput input, CancellationToken ctx);

    Task UpdateSpeakerAsync(int id, SpeakerModelInput input, CancellationToken ctx);

    Task DeleteSpeakerAsync(int id, CancellationToken ctx);
}
