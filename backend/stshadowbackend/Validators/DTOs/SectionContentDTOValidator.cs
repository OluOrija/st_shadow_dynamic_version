using FluentValidation;
using stshadowbackend.DTO;


namespace stshadowbackend.Validators.DTOs
{
    public class SectionContentDTOValidator : AbstractValidator<SectionContentDTO>
    {
        public SectionContentDTOValidator()
        {
            RuleFor(x => x.Title)
                .NotEmpty().WithMessage("Title is required.")
                .MaximumLength(200).WithMessage("Title cannot exceed 200 characters.");

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage("Description is required.")
                .MaximumLength(1000).WithMessage("Description cannot exceed 1000 characters.");

            RuleFor(x => x.ImageUrl)
                .MaximumLength(500).WithMessage("Image URL cannot exceed 500 characters.")
                .When(x => !string.IsNullOrEmpty(x.ImageUrl)); // Validate only if ImageUrl is provided.

            RuleFor(x => x.Order)
                .GreaterThanOrEqualTo(0).WithMessage("Order must be zero or a positive number.");
        }
    }

}
