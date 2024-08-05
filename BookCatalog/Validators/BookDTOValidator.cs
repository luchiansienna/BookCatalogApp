using BookCatalog.Services.DTO;
using FluentValidation;

namespace BookCatalog.Validators
{
        public class BookDTOValidator : AbstractValidator<BookDTO>
        {
            public BookDTOValidator()
            {
                RuleFor(dto => dto.Title).NotEmpty().MaximumLength(100);
                RuleFor(dto => dto.Publisher).NotEmpty().SetValidator(new PublisherDTOValidator());
                RuleFor(dto => dto.Edition).NotEmpty().MinimumLength(3).MaximumLength(50);
                RuleFor(dto => dto.PublishedDate).NotEmpty();
                RuleForEach(dto => dto.Authors).SetValidator(new AuthorDTOValidator());
            }
        }
    
}
