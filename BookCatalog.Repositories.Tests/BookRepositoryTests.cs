using BookCatalog.Domain;
using BookCatalog.Repositories.Contracts;

namespace BookCatalog.Repositories.Tests
{
    public class BookRepositoryTest
    {
        private IBookRepository bookRepository;

        [SetUp]
        public void SetUp()
        {
            bookRepository = BookRepositoryMock.GetMock();
        }

        [Test]
        public void GetBooks()
        {
            //Arrange


            //Act
            IEnumerable<Book> lstData = bookRepository.GetAll();


            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(lstData, Is.Not.Null);
                Assert.That(lstData.Count, Is.GreaterThan(0));
            });
        }

        [Test]
        public void GetBookById()
        {
            //Arrange
            int id = 1;

            //Act
            Book data = bookRepository.Get(id);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(data, Is.Not.Null);
                Assert.That(data.Id, Is.EqualTo(id));
            });
        }

        [Test]
        public void AddBook()
        {
            //Arrange
            var authorList = new List<Author>();
            authorList.Add(new Author() { Name = "John", Surname = "McGregor", Birthyear = 1986 });
            authorList.Add(new Author() { Name = "Mike", Surname = "Sally", Birthyear = 1976 });

            Book book = new Book()
            {
                Id = 100,
                Title = "New Book",
                Authors = authorList,
                Publisher = new Publisher() { Name = "Star Publisher" },
                Edition = "First edition",
                PublishedDate = new DateOnly(2010, 1, 1)
            };

            //Act
            Book expectedData = bookRepository.Save(book);
            Book returnedExpectedData = bookRepository.Get(expectedData.Id);

            //Assert
            Assert.Multiple(() =>
            {
                
                Assert.That(expectedData, Is.Not.Null);
                Assert.That(returnedExpectedData, Is.Not.Null);
                Assert.That(returnedExpectedData.Id, Is.EqualTo(expectedData.Id));
            });
        }

        [Test]
        public void UpdateBook()
        {
            //Arrange
            var authorList = new List<Author>();
            authorList.Add(new Author() { Name = "John", Surname = "McGregor", Birthyear = 1986 });
            authorList.Add(new Author() { Name = "Mike", Surname = "Sally", Birthyear = 1976 });

            int id = 2;
            Book actualData = bookRepository.Get(id);
            actualData.Title = "Title of the Book";
            actualData.Edition = "Edition number 1";
            actualData.Authors = authorList;
            actualData.Publisher = new Publisher() { Name = "Star Publisher" };
            actualData.PublishedDate = new DateOnly(2010, 1, 1);
            //Act
            Book expectedData = bookRepository.Update(actualData);
            Book returnedExpectedData = bookRepository.Get(actualData.Id);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(expectedData, Is.Not.Null);
                Assert.That(expectedData, Is.EqualTo(actualData));
                Assert.That(returnedExpectedData, Is.EqualTo(actualData));
            });
        }

        [Test]
        public void DeleteBook()
        {
            //Arrange
            int id = 2;
            Book actualData = bookRepository.Get(id);

            //Act
            bookRepository.Remove(actualData);
            Book expectedData = bookRepository.Get(actualData.Id);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(expectedData, Is.Null);
            });
        }
    }
}
