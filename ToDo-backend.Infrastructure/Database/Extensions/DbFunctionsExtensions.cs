namespace ToDo_backend.Infrastructure.Database.Extensions;

using Microsoft.EntityFrameworkCore;

public static class DbFunctionsExtensions
{
    /// <summary>  
    /// Removes diacritics (accents) from the input string to enable searches.  
    /// This function is implemented at the database level.
    /// The implementation can be found in the file: <see cref="Database.Functions.AddNormalizeTextFunction.cs" />
    /// </summary>
    [DbFunction("remove_diacritics", IsBuiltIn = false)]
    public static string RemoveDiacritics(string input)
    {
        throw new InvalidOperationException("This method can only be used in LINQ-to-Entities queries.");
    }
}