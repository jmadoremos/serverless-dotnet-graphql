namespace GraphQL.WebAPI.Repositories.Responses;

public class SwapiResponseList<T>
    where T : SwapiResponse
{
    public int Count { get; set; } = default!;

    public string? Next { get; set; } = default!;

    public string? Previous { get; set; } = default!;

    public IEnumerable<T> Results { get; set; } = [];
}
