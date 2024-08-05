using BookCatalog.Services.DTO;
using FluentValidation;

namespace BookCatalog.Validators
{
    public class AuthorDTOValidator : AbstractValidator<AuthorDTO>
    {
        public AuthorDTOValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().MinimumLength(3).MaximumLength(60);
            RuleFor(dto => dto.Surname).NotEmpty().MinimumLength(3).MaximumLength(60);
            RuleFor(dto => dto.Birthyear).NotEmpty();
        }
    }

}
