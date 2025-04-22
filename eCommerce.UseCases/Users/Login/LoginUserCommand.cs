using eCommerce.Core.Common;
using MediatR;

namespace eCommerce.UseCases.Users.Login;

public record LoginUserCommand(string Email , string Password)
    :IRequest<Result<bool>>;
