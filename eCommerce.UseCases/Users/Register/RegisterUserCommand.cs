using eCommerce.Core.Common;
using MediatR;

namespace eCommerce.UseCases.Users.Register;

public record RegisterUserCommand(string Email, string Name,string Password) : IRequest<Result<Guid>>;