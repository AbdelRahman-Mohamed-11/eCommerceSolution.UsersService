using System.Data;
using Microsoft.Extensions.Configuration;
using Npgsql;

namespace eCommerce.Infrastructure.DbContext;

public class DapperDbContext
{
    public IDbConnection Connection { get; }

    public DapperDbContext(IConfiguration configuration)
    {
        var connectionString = configuration.GetConnectionString("PostgresConnection")
            ?? throw new ArgumentNullException("PostgresConnection string not found.");

        Connection = new NpgsqlConnection(connectionString);
    }
}
