using eCommerce.API.Requests.Users;
using eCommerce.UseCases.Users.Login;
using eCommerce.UseCases.Users.Register;
using FluentValidation;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using eCommerce.API.Extensions;

namespace eCommerce.API.Controllers;

public class UsersController(IMediator mediator) : BaseApiController
{
    [HttpPost("register")]
    public async Task<IActionResult> Register(RegisterUserRequest registerUserRequest, IValidator<RegisterUserCommand> validator)
    {
        var command = new RegisterUserCommand(registerUserRequest.Email, registerUserRequest.Name, registerUserRequest.Password);

        var validateResult = validator.Validate(command);

        if (!validateResult.IsValid)
        {
            return this.ValidationProblem(validateResult);
        }

        var result = await mediator.Send(command);
        return result.ToActionResult(this, nameof(GetUser));
    }

    [HttpPost("login")]
    public async Task<IActionResult> Login(LoginUserRequest loginUserRequest, IValidator<LoginUserCommand> validator)
    {
        var command = new LoginUserCommand(loginUserRequest.Email, loginUserRequest.Password);

        var validateResult = validator.Validate(command);

        if (!validateResult.IsValid)
        {
            return this.ValidationProblem(validateResult);
        }

        var result = await mediator.Send(command);
        return result.ToActionResult(this);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetUser(Guid id)
    {
        // Implementation for getting a user by ID
        return Ok();
    }
}