namespace GraphQL.Database.Repositories.Speakers;

using ErrorOr;

public interface ISpeakerRepository
{
    Task<IQueryable<SpeakerModel>> GetAllSpeakersAsync(CancellationToken ctx);

    Task<SpeakerModel?> GetSpeakerByIdAsync(int id, CancellationToken ctx);

    Task<IQueryable<SpeakerModel>> GetSpeakersByIdsAsync(IEnumerable<int> ids, CancellationToken ctx);

    Task<SpeakerModel?> GetSpeakerByNameAsync(string name, CancellationToken ctx);

    Task<ErrorOr<int>> CreateSpeakerAsync(SpeakerModelInput input, CancellationToken ctx);

    Task<ErrorOr<Updated>> UpdateSpeakerAsync(int id, SpeakerModelInput input, CancellationToken ctx);

    Task<ErrorOr<Deleted>> DeleteSpeakerAsync(int id, CancellationToken ctx);
}
