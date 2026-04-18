using System.Data;
using Microsoft.Data.SqlClient;

namespace Infrastructure.Data;

public class SqlConnectionFactory : IDbConnectionFactory
{
    private readonly string _connectionString;

    public SqlConnectionFactory(string connectionString)
    {
        _connectionString = connectionString;
    }

    public IDbConnection Create() => new SqlConnection(_connectionString);
}
