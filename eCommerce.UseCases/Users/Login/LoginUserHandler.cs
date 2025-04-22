using eCommerce.Core.Common;
using eCommerce.Core.Interfaces;
using MediatR;

namespace eCommerce.UseCases.Users.Login;

public class LoginUserHandler(IUsersRepository usersRepository, IPasswordHasher passwordHasher) : IRequestHandler<LoginUserCommand, Result<bool>>
{
    public async Task<Result<bool>> Handle(LoginUserCommand request, CancellationToken cancellationToken)
    {
        var user = await usersRepository.GetUserByEmailAsync(request.Email);

        if (user is null)
            return Result<bool>.Invalid("Invalid email or password.");

        var passwordValid = passwordHasher.VerifyPassword(user.Password, request.Password);

        return passwordValid
            ? Result<bool>.Success(true)
            : Result<bool>.UnAuthorized("Invalid email or password.");
    }
}
