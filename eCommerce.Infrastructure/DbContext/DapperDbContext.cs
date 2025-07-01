using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace eCommerce.Infrastructure.DbContext;

public class DapperDbContext
{
    public IDbConnection Connection { get; }

    public DapperDbContext(IConfiguration configuration)
    {
        var connectionStringTemplate = configuration.GetConnectionString("PostgresConnection")
            ?? throw new ArgumentNullException("PostgresConnection string not found.");

        string connectionString = connectionStringTemplate
            .Replace("$POSTGRES_HOST", Environment.GetEnvironmentVariable("POSTGRES_HOST"))
            .Replace("$POSTGRES_PASSWORD", Environment.GetEnvironmentVariable("POSTGRES_PASSWORD"));


        Connection = new NpgsqlConnection(connectionString);
    }
}
