namespace ToDo_backend.Application.Common.Responses;


#region Usings
using System.Text.Json.Serialization;
using ToDo_backend.Application.Common.Pagination;
#endregion

/// <summary>
/// Standard API response wrapper for paginated data collections
/// </summary>
/// <typeparam name="T">The type of data being returned</typeparam>
public sealed class ApiResponse<T>
{
    /// <summary>
    /// The main data payload - can be a collection, single object, or null
    /// </summary>
    public T Data { get; init; } = default!;

    /// <summary>
    /// Pagination metadata - only included for paginated responses
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public PaginationMetadata? Pagination { get; init; }

    /// <summary>
    /// Additional properties for future extensibility
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? ExtraProperties { get; init; }

    /// <summary>
    /// Internal constructor for factory method usage
    /// </summary>
    internal ApiResponse() { }

    /// <summary>
    /// Creates a simple API response without pagination
    /// </summary>
    /// <param name="data">The data to wrap</param>
    /// <param name="extraProperties">Optional additional properties</param>
    /// <returns>A new ApiResponse without pagination</returns>
    public static ApiResponse<T> FromData(
        T data, 
        Dictionary<string, object>? extraProperties = null)
    {
        return new ApiResponse<T>
        {
            Data = data,
            ExtraProperties = extraProperties
        };
    }

    /// <summary>
    /// Creates an empty API response (data = null)
    /// </summary>
    /// <param name="extraProperties">Optional additional properties</param>
    /// <returns>A new ApiResponse with null data</returns>
    public static ApiResponse<T> Empty(Dictionary<string, object>? extraProperties = null)
    {
        return new ApiResponse<T>
        {
            Data = default!,
            ExtraProperties = extraProperties
        };
    }
}

/// <summary>
/// Static factory methods for ApiResponse
/// </summary>
public static class ApiResponse
{
    /// <summary>
    /// Creates a paginated API response from a PagedList
    /// </summary>
    /// <param name="pagedList">The paged data</param>
    /// <param name="extraProperties">Optional additional properties</param>
    /// <returns>A new ApiResponse with pagination metadata</returns>
    public static ApiResponse<List<T>> FromPagedList<T>(
        PaginatedResult<T> pagedList, 
        Dictionary<string, object>? extraProperties = null)
    {
        return new ApiResponse<List<T>>
        {
            Data = pagedList.Data,
            Pagination = pagedList.Pagination,
            ExtraProperties = extraProperties
        };
    }
}
