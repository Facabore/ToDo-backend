namespace ToDo_backend.Application.Common.Abstractions.Data;

using System.Data;

public interface IDbConnectionFactory
{
    IDbConnection CreateConnection();
}