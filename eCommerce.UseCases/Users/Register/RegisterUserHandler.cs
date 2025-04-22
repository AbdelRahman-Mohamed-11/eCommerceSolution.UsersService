using eCommerce.Core.Common;
using eCommerce.Core.Entities;
using eCommerce.Core.Interfaces;
using MediatR;

namespace eCommerce.UseCases.Users.Register;

public class RegisterUserHandler(IUsersRepository usersRepository , 
    IPasswordHasher passwordHasher) : IRequestHandler<RegisterUserCommand, Result<Guid>>
{
    public async Task<Result<Guid>> Handle(RegisterUserCommand request, CancellationToken cancellationToken)
    {
        var existingUser = await usersRepository.GetUserByEmailAsync(request.Email);

        if (existingUser is not null)
        {
            return Result<Guid>.Conflict("User Already Exists");
        }

        var hashedPassword = passwordHasher.HashPassword(request.Password);

        if(string.IsNullOrEmpty(hashedPassword))
        {
            return Result<Guid>.Failure("Failed to hash password");
        }

        var user = new ApplicationUser { Email = request.Email, Name = request.Name, Password = hashedPassword };

        var createdUserId = await usersRepository.AddUserAsync(user);

        return Result<Guid>.Created(createdUserId);
    }
}
