﻿namespace eCommerce.API.Requests.Users;

public class RegisterUserRequest
{
    public required string Email { get; set; }
    public required string Name { get; set; }
    public required string Password { get; set; }
}
