using BookCatalog.Domain;
using BookCatalog.Repositories.Contracts;
using Moq;

namespace BookCatalog.Repositories.Tests
{
    public class BookRepositoryMock
    {
        public static IBookRepository GetMock()
        {
            List<Book> lstBook = GenerateTestData();
            var dbContextMock = DbContextMock.GetMock(new Mock<BookCatalogContext>(), lstBook, x => x.Book);
            return new BookRepository(dbContextMock.Object);
        }

        private static List<Book> GenerateTestData()
        {
            var bookList = new List<Book>();
            var authorList = new List<Author>();
            authorList.Add(new Author() { Name = "John", Surname = "McGregor", Birthyear = 1986 });
            authorList.Add(new Author() { Name = "Mike", Surname = "Sally", Birthyear = 1976 });

            for (int index = 1; index <= 10; index++)
            {
                bookList.Add(new Book
                {
                    Id = index,
                    Title = "Book-" + index,
                    Authors = authorList,
                    Publisher = new Publisher() { Name = "Star Publisher " + index },
                    Edition = "First edition",
                    PublishedDate = new DateOnly(2010, 1, 1)
                });
            }
            return bookList;
        }
    }
}
