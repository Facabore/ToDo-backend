namespace ToDo_backend.Infrastructure.Database;

using ToDo_backend.Application.Common.Abstractions.Data;
using System.Data;

internal sealed class DbConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public DbConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection CreateConnection()
    {
        var connection = new Npgsql.NpgsqlConnection(_connectionString);
        connection.Open();

        return connection;
    }
}