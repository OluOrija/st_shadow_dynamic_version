using FluentValidation;
using stshadowbackend.Models;


namespace stshadowbackend.Validators.Models
{
    public class UserValidator : AbstractValidator<User>
    {
        public UserValidator()
        {
            RuleFor(x => x.Username)
                .NotEmpty().WithMessage("Username is required.")
                .MaximumLength(50).WithMessage("Username cannot exceed 50 characters.");

            RuleFor(x => x.Password)
                .NotEmpty().WithMessage("Password is required.")
                .MinimumLength(8).WithMessage("Password must be at least 8 characters long.");

            RuleFor(x => x.Role)
                .Must(role => role == "Admin" || role == "Editor" || role == "Viewer" || string.IsNullOrEmpty(role))
                .WithMessage("Role must be 'Admin', 'Editor', or 'Viewer'.");
        }
    }

}
