namespace ToDo_backend.Application.Common.Pagination;

public record PaginationDetails(
    int CurrentPage,
    int PageSize,
    int TotalItems,
    int TotalPages,
    bool HasNextPage,
    bool HasPreviousPage);