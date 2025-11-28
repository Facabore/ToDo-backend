namespace ToDo_backend.Application.Common.Pagination;

using Microsoft.EntityFrameworkCore;
using ToDo_backend.Application.Common.Responses;

public class PaginatedResult<T>
{
    #region Constants
    private const int One = 1; 
    #endregion

    private PaginatedResult(List<T> data, PaginationMetadata pagination)
    {
        Data = data;
        Pagination = pagination;
    }

    public List<T> Data { get; }

    public PaginationMetadata Pagination { get; }

    public static async Task<PaginatedResult<T>> CreateAsync(IQueryable<T> query, int page, int pageSize)
    {
        var totalItems = await query.CountAsync();
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var pagination = new PaginationMetadata(
            page,
            pageSize,
            totalItems,
            totalPages,
            page * pageSize < totalItems,
            page > One
        );

        var data = await query.Skip((page - One) * pageSize).Take(pageSize).ToListAsync();

        return new(data, pagination);
    }

    public static PaginatedResult<T> Create(List<T> data, int page, int pageSize, int totalItems)
    {
        var totalPages = (int)Math.Ceiling(totalItems / (double)pageSize);

        var pagination = new PaginationMetadata(
            page,
            pageSize,
            totalItems,
            totalPages,
            page * pageSize < totalItems,
            page > One
        );

        return new(data, pagination);
    }
}