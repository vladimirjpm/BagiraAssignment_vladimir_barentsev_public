namespace Backend.Application.Search;

public interface ISearchService
{
    Task<SearchResultDto> SearchAsync(string query, CancellationToken ct);
}
