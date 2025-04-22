using Dapper;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces;
using eCommerce.Infrastructure.DbContext;

namespace eCommerce.Infrastructure.Repositories;

public class UsersRepository(DapperDbContext dapperDbContext) : IUsersRepository
{
    public async Task<Guid> AddUserAsync(ApplicationUser user)
    {
        const string sql = @"
            INSERT INTO public.""Users"" (""Name"", ""Email"", ""Password"")
            VALUES (@Name, @Email, @Password)
            RETURNING ""Id"";
        ";

        var userId = await dapperDbContext.Connection.ExecuteScalarAsync<Guid>(sql, new
        {
            user.Name,
            user.Email,
            user.Password
        });

        return userId;
    }

    public async Task<ApplicationUser?> GetUserByEmailAsync(string email)
    {
        const string sql = @"SELECT * FROM public.""Users"" WHERE ""Email"" = @Email";

        var user = await dapperDbContext.Connection.QueryFirstOrDefaultAsync<ApplicationUser>(sql, new { Email = email });

        return user;
    }
}
