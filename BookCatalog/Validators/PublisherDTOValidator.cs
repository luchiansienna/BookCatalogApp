using BookCatalog.Services.DTO;
using FluentValidation;

namespace BookCatalog.Validators
{
    public class PublisherDTOValidator : AbstractValidator<PublisherDTO>
    {
        public PublisherDTOValidator()
        {
            RuleFor(dto => dto.Name).NotEmpty().MinimumLength(3).MaximumLength(50);
        }
    }
}
