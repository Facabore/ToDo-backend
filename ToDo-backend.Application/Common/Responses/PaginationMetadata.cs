namespace ToDo_backend.Application.Common.Responses;

public sealed record PaginationMetadata
{
    public PaginationMetadata(
        int page,
        int pageSize,
        int totalItems,
        int totalPages,
        bool hasNextPage,
        bool hasPreviousPage)
    {
        Page = page;
        PageSize = pageSize;
        TotalItems = totalItems;
        TotalPages = totalPages;
        HasNextPage = hasNextPage;
        HasPreviousPage = hasPreviousPage;
    }

    public int Page { get; init; }

    public int PageSize { get; init; }

    public int TotalItems { get; init; }

    public int TotalPages { get; init; }

    public bool HasNextPage { get; init; }

    public bool HasPreviousPage { get; init; }
}
