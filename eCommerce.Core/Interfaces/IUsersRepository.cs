using eCommerce.Core.Entities;

namespace eCommerce.Core.Interfaces;

public interface IUsersRepository
{
    Task<Guid> AddUserAsync(ApplicationUser user);

    Task<ApplicationUser?> GetUserByEmailAsync(string email);
}