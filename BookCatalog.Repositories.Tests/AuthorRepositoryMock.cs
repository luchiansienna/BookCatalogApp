using BookCatalog.Domain;
using BookCatalog.Repositories.Contracts;
using Microsoft.EntityFrameworkCore;
using Moq;

namespace BookCatalog.Repositories.Tests
{
    public class AuthorRepositoryMock
    {
        public static IAuthorRepository GetMock()
        {
            List<Author> lstAuthor = GenerateTestData();
            var dbContextMock = DbContextMock.GetMock(new Mock<BookCatalogContext>(), lstAuthor, x => x.Author);
            return new AuthorRepository(dbContextMock.Object);
        }

        private static List<Author> GenerateTestData()
        {
            var authorList = new List<Author>();
            authorList.Add(new Author() { Id = 1, Name = "John", Surname = "McGregor", Birthyear = 1986 });
            authorList.Add(new Author() { Id = 2, Name = "Mike", Surname = "Sally", Birthyear = 1976 });

            return authorList;
        }
    }
}
