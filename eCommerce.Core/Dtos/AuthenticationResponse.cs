namespace eCommerce.Core.Dtos;

public record AuthenticationResponse(
    Guid Id,
    string? Email,
    string? Token,
    bool Success
);
