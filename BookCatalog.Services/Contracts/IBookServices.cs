using BookCatalog.Domain;
using BookCatalog.Services.DTO;

namespace BookCatalog.Services.Contracts
{
    public interface IBookServices : IBaseService<Book, BookDTO>
    {
    }
}
