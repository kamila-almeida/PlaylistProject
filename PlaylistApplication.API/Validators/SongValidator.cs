using FluentValidation;
using PlaylistApplication.API.Entities;

namespace PlaylistApplication.API.Validators
{
    public class SongValidator : AbstractValidator<Song>
    {
        public static string NameErrorMessage = "Name is required.";
        public static string AuthorErrorMessage = "Author is required.";

        public SongValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage(NameErrorMessage)
                .NotNull().WithMessage(NameErrorMessage);

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage(AuthorErrorMessage)
                .NotNull().WithMessage(AuthorErrorMessage);
        }
    }
}
