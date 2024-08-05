using BookCatalog.Domain;
using BookCatalog.Repositories.Contracts;

namespace BookCatalog.Repositories.Tests
{
    public class AuthorRepositoryTest
    {
        private IAuthorRepository authorRepository;

        [SetUp]
        public void SetUp()
        {
            authorRepository = IAuthorRepositoryMock.GetMock();
        }

        [Test]
        public void GetAuthors()
        {
            //Arrange


            //Act
            IEnumerable<Author> lstData = authorRepository.GetAll();


            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(lstData, Is.Not.Null);
                Assert.That(lstData.Count, Is.GreaterThan(0));
            });
        }

        [Test]
        public void GetAuthorById()
        {
            //Arrange
            int id = 1;

            //Act
            Author data = authorRepository.Get(id);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(data, Is.Not.Null);
                Assert.That(data.Id, Is.EqualTo(id));
            });
        }

        [Test]
        public void AddAuthor()
        {
            //Arrange
            Author author = new Author()
            {
                Id = 3,
                Name = "Simon",
                Surname = "Peter",
                Birthyear = 1986
            };

            //Act
            Author expectedData = authorRepository.Save(author);
            Author returnedExpectedData = authorRepository.Get(expectedData.Id);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(expectedData, Is.Not.Null);
                Assert.That(returnedExpectedData, Is.Not.Null);
                Assert.That(returnedExpectedData.Id, Is.EqualTo(expectedData.Id));
            });
        }

        [Test]
        public void UpdateAuthor()
        {
            //Arrange
            int id = 2;
            Author actualData = authorRepository.Get(id);
            actualData.Name = "Andrew";
            actualData.Surname = "Simon";
            actualData.Birthyear = 1986;
            //Act
            Author expectedData = authorRepository.Update(actualData);
            Author returnedExpectedData = authorRepository.Get(actualData.Id);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(expectedData, Is.Not.Null);
                Assert.That(expectedData, Is.EqualTo(actualData));
                Assert.That(returnedExpectedData, Is.EqualTo(actualData));
            });
        }

        [Test]
        public void DeleteAuthor()
        {
            //Arrange
            int id = 2;
            Author actualData = authorRepository.Get(id);

            //Act
            authorRepository.Remove(actualData);
            Author expectedData = authorRepository.Get(actualData.Id);

            //Assert
            Assert.Multiple(() =>
            {
                Assert.That(expectedData, Is.Null);
            });
        }
    }
}
