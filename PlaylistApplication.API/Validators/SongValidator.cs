using FluentValidation;
using PlaylistApplication.API.Entities;

namespace PlaylistApplication.API.Validators
{
    public class SongValidator : AbstractValidator<Song>
    {
        public SongValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Name is required.")
                .NotNull().WithMessage("Name is required.");

            RuleFor(x => x.Author)
                .NotEmpty().WithMessage("Author is required.")
                .NotNull().WithMessage("Author is required.");
        }
    }
}
