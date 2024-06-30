namespace WMS.WebUI.Models;

public class PaginatedResponse<T>
{
    public List<T> Data { get; init; }
    public int CurrentPage { get; init; }
    public int PageSize { get; init; }
    public int PagesCount { get; init; }
    public int TotalCount { get; init; }
    public bool HasNextPage { get; init; }
    public bool HasPreviousPage { get; init; }

    public PaginatedResponse()
    {
        Data = [];
    }
}
