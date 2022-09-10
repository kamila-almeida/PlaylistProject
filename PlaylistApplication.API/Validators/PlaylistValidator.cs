using FluentValidation;
using PlaylistApplication.API.Entities;

namespace PlaylistApplication.API.Validators
{
    public class PlaylistValidator : AbstractValidator<Playlist>
    {
        public static string NameErrorMessage = "Name is required.";
        public static string DescriptionErrorMessage = "Description is required.";

        public PlaylistValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(NameErrorMessage)
                .NotNull().WithMessage(NameErrorMessage);

            RuleFor(x => x.Description)
                .NotEmpty().WithMessage(DescriptionErrorMessage)
                .NotNull().WithMessage(DescriptionErrorMessage);
        }
    }
}
