namespace ToDo_backend.Application.Common.Responses;

#region Usings
using ToDo_backend.Application.Common.Pagination;
#endregion

/// <summary>
/// Extension methods to facilitate the creation of standardized API responses
/// </summary>
public static class ApiResponseExtensions
{
    /// <summary>
    /// Converts a PagedList to an ApiResponse with pagination
    /// </summary>
    /// <typeparam name="T">Type of the items in the list</typeparam>
    /// <param name="pagedList">The paged list to convert</param>
    /// <param name="extraProperties">Optional additional properties</param>
    /// <returns>An ApiResponse with pagination metadata</returns>
    public static ApiResponse<List<T>> ToApiResponse<T>(
        this PaginatedResult<T> pagedList,
        Dictionary<string, object>? extraProperties = null)
    {
        return ApiResponse.FromPagedList(pagedList, extraProperties);
    }

    /// <summary>
    /// Converts any data to a simple ApiResponse without pagination
    /// </summary>
    /// <typeparam name="T">Type of the data</typeparam>
    /// <param name="data">The data to wrap</param>
    /// <param name="extraProperties">Optional additional properties</param>
    /// <returns>An ApiResponse without pagination</returns>
    public static ApiResponse<T> ToApiResponse<T>(
        this T data,
        Dictionary<string, object>? extraProperties = null)
    {
        return ApiResponse<T>.FromData(data, extraProperties);
    }

    /// <summary>
    /// Converts any data to a SimpleApiResponse
    /// </summary>
    /// <typeparam name="T">Type of the data</typeparam>
    /// <param name="data">The data to wrap</param>
    /// <param name="extraProperties">Optional additional properties</param>
    /// <returns>A SimpleApiResponse</returns>
    public static SimpleApiResponse<T> ToSimpleApiResponse<T>(
        this T data,
        Dictionary<string, object>? extraProperties = null)
    {
        return SimpleApiResponse<T>.FromData(data, extraProperties);
    }

    /// <summary>
    /// Creates an empty ApiResponse
    /// </summary>
    /// <typeparam name="T">Type of the response</typeparam>
    /// <param name="extraProperties">Optional additional properties</param>
    /// <returns>An empty ApiResponse</returns>
    public static ApiResponse<T> EmptyApiResponse<T>(Dictionary<string, object>? extraProperties = null)
    {
        return ApiResponse<T>.Empty(extraProperties);
    }

    /// <summary>
    /// Creates an empty SimpleApiResponse
    /// </summary>
    /// <typeparam name="T">Type of the response</typeparam>
    /// <param name="extraProperties">Optional additional properties</param>
    /// <returns>An empty SimpleApiResponse</returns>
    public static SimpleApiResponse<T> EmptySimpleApiResponse<T>(Dictionary<string, object>? extraProperties = null)
    {
        return SimpleApiResponse<T>.Empty(extraProperties);
    }

    /// <summary>
    /// Creates a success response for operations that don't return data
    /// </summary>
    /// <param name="message">Success message</param>
    /// <param name="extraProperties">Optional additional properties</param>
    /// <returns>A success SimpleApiResponse</returns>
    public static SimpleApiResponse<string> SuccessResponse(
        string message = "Operation completed successfully",
        Dictionary<string, object>? extraProperties = null)
    {
        return SimpleApiResponse<string>.Success(message, extraProperties);
    }
}
