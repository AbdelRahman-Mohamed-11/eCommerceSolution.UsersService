namespace eCommerce.Core.Entities;

public class ApplicationUser
{
    public Guid Id { get; set; }
    public required string Email { get; set; }
    public required string Password { get; set; }
    public required string Name { get; set; }
}
