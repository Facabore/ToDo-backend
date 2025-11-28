namespace ToDo_backend.Application.Common.Responses;

#region Usings
using System.Text.Json.Serialization;
#endregion

/// <summary>
/// Simplified API response wrapper for single resources or simple operations
/// </summary>
/// <typeparam name="T">The type of data being returned</typeparam>
public sealed class SimpleApiResponse<T>
{
    /// <summary>
    /// The main data payload - can be a single object or null
    /// </summary>
    public T Data { get; init; } = default!;

    /// <summary>
    /// Additional properties for future extensibility
    /// </summary>
    [JsonIgnore(Condition = JsonIgnoreCondition.WhenWritingNull)]
    public Dictionary<string, object>? ExtraProperties { get; init; }

    /// <summary>
    /// Internal constructor to enforce factory method usage
    /// </summary>
    internal SimpleApiResponse() { }

    /// <summary>
    /// Creates a simple API response with data
    /// </summary>
    /// <param name="data">The data to wrap</param>
    /// <param name="extraProperties">Optional additional properties</param>
    /// <returns>A new SimpleApiResponse</returns>
    public static SimpleApiResponse<T> FromData(
        T data, 
        Dictionary<string, object>? extraProperties = null)
    {
        return new SimpleApiResponse<T>
        {
            Data = data,
            ExtraProperties = extraProperties
        };
    }

    /// <summary>
    /// Creates an empty simple API response (data = null)
    /// </summary>
    /// <param name="extraProperties">Optional additional properties</param>
    /// <returns>A new SimpleApiResponse with null data</returns>
    public static SimpleApiResponse<T> Empty(Dictionary<string, object>? extraProperties = null)
    {
        return new SimpleApiResponse<T>
        {
            Data = default!,
            ExtraProperties = extraProperties
        };
    }

    /// <summary>
    /// Creates a success response for operations that don't return data
    /// </summary>
    /// <param name="message">Success message</param>
    /// <param name="extraProperties">Optional additional properties</param>
    /// <returns>A new SimpleApiResponse with success message</returns>
    public static SimpleApiResponse<string> Success(
        string message = "Operation completed successfully",
        Dictionary<string, object>? extraProperties = null)
    {
        return new SimpleApiResponse<string>
        {
            Data = message,
            ExtraProperties = extraProperties
        };
    }
}
