using FluentValidation;
using stshadowbackend.DTO;


namespace stshadowbackend.Validators.DTOs
{
    public class MediaAssetsDTOValidator : AbstractValidator<MediaAssetsDTO>
    {
        public MediaAssetsDTOValidator()
        {
            RuleFor(x => x.FileName)
                .NotEmpty().WithMessage("File name is required.")
                .MaximumLength(255).WithMessage("File name cannot exceed 255 characters.");

            RuleFor(x => x.FilePath)
                .NotEmpty().WithMessage("File path is required.")
                .MaximumLength(500).WithMessage("File path cannot exceed 500 characters.");
        }
    }

}
