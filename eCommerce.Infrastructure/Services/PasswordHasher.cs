using eCommerce.Core.Interfaces;


namespace eCommerce.Infrastructure.Services;

public class PasswordHasher : IPasswordHasher
{
    public string HashPassword(string password)
        => BCrypt.Net.BCrypt.HashPassword(password);

    public bool VerifyPassword(string hashedPassword, string inputPassword)
        => BCrypt.Net.BCrypt.Verify(inputPassword, hashedPassword);
}
