using BookCatalog.Domain;
using BookCatalog.Services.DTO;

namespace BookCatalog.Services.Contracts
{
    public interface IAuthorServices : IBaseService<Author, AuthorDTO>
    {
    }
}
