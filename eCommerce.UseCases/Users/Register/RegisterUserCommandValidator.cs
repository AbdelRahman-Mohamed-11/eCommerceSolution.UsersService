using FluentValidation;

namespace eCommerce.UseCases.Users.Register
{
    public class RegisterUserCommandValidator : AbstractValidator<RegisterUserCommand>
    {
        public RegisterUserCommandValidator()
        {
            RuleFor(x => x.Email)
                .EmailAddress().WithMessage("Invalid email format.");

            RuleFor(x => x.Name)
                .MinimumLength(3).WithMessage("Name must be at least 3 characters.");


            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(6).WithMessage("Password must be at least 6 characters.")
                .Matches("[A-Z]").WithMessage("Password must contain at least one uppercase letter.")
                .Matches("[a-z]").WithMessage("Password must contain at least one lowercase letter.")
                .Matches(@"\d").WithMessage("Password must contain at least one digit.")
                .Matches(@"[!@#$%^&*()_+\-=\[\]{};:'""<>,./?\\|]").WithMessage("Password must contain at least one special character.");
        }
    }
}
