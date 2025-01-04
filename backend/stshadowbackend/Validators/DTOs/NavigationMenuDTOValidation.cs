using stshadowbackend.DTO;
using FluentValidation;

namespace stshadowbackend.Validators.DTOs
{
    public class NavigationMenuDTOValidator : AbstractValidator<NavigationMenuDTO>
    {
        public NavigationMenuDTOValidator()
        {
            RuleFor(x => x.Name).NotEmpty().MaximumLength(100);
            RuleFor(x => x.Url).NotEmpty().MaximumLength(200);
            RuleFor(x => x.Order).GreaterThan(0);
        }
    }

}
