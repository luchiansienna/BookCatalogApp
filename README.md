# Book Catalog app

This is Web Api application that manages a Book Catalog .
The test description is located <a id='ssFeatures' href='https://github.com/luchiansienna/BookCatalogApp/blob/main/BookCatalog/Exercise.md'>here</a>
The POSTMAN file for the API calls is located <a href='https://github.com/luchiansienna/BookCatalogApp/blob/main/BookCatalog/Dataspartan.postman_collection.json'>here</a>

This app is developed using C# .NET Core framework.
I have used CODE FIRST approach for the database with Entity Framework.
The database used is Microsoft SQL.



## Steps to set the app on your local machine

1. **Install .Net Framework Core & SQL Express database Server instance** on your machine.
Check if **appsettings.json** file located in the **BookCatalog** project has the right database connection string and adjust accordingly. It should point to your newly created SQL Express instance.
The connection string is named **BookCatalogContext** and has the default value : ""BookCatalogContext": "Server=(local)\\SQLEXPRESS;Database=BookCatalog;Trusted_Connection=true;MultipleActiveResultSets=true;TrustServerCertificate=True;"

3. Open the solution in **Visual Studio** and Run '**update-database**' command into 'Package Manager Console' with **BookCatalog.Repositories** project selected to install the database schema ( apply migration ) into your installed local SQL express database

4. Optional: To browse directly through the database you may want to install **Sql Server Management Studio**

5. Run your app locally on IIS Express or any other .NET web server

The Swagger start up page will help in calling the Book and Author RESTFul API.

Start by creating a new book, by hitting POST on the /book API Endpoint with the following JSON or something similar:

  ```sh
{
    "title": "The Bible",
    "authors": [
      {
        "name": "Moses",
        "surname": "The Jew",
        "birthyear": 1
      },
      {
        "name": "John",
        "surname": "The Apostle",
        "birthyear": 50
      }
    ],
    "publisher": {
      "name": "God"
    },
    "edition": "The Eternal Edition",
    "publishedDate": "2001-08-03"
}
  ```

This will create a book , 2 authors and one publisher.

You can also add book without authors

  ```sh
{
    "title": "The Quran",
    "authors": [
    ],
    "publisher": {
      "name": "Publisher"
    },
    "edition": "The Eternal Edition",
    "publishedDate": "2001-08-03"
}
  ```

Books with existing authors can be added as well, but the id of the author must be specified

  ```sh
{
    "title": "The Quran",
    "authors": [
      {
        "id" : 1,
        "name": "Moses",
        "surname": "The Jew",
        "birthyear": 1
      },
    ],
    "publisher": {
      "name": "Publisher"
    },
    "edition": "The Eternal Edition",
    "publishedDate": "2001-08-03"
}
  ```

If you hit GET on /book endpoint you will be able to get the ids of the authors or the publisher for referencing them in future POSTs or PUTs.

Then you can go ahead and copy the fetched JSON that contains info about the just added book , that also contains the ids of the entities registered in the database.

A PUT on the /book endpoint with the copied JSON with some additional changes will update the book and it list of authors. 

Books can be deleted as well on the /book endpoint and all books with the corresponding links will be removed, but the related authors and publisher will remain in place

When removing authors on the /author endpoint, a check if the author has existing books.
The author cannot be deleted if there are books that relate to the author.
To delete the author, please remove all its corresponding books or remove the author from the books manually by updating the books.

## Validations are in place for API requests

With the help of FluentValidation library, JSON validations are in place in the **BookCatalog.Validators** namespace

## Automapper library used for mapping DTOs to Models

The MappingProfiles in BookCatalog.Services takes care of the mappings setup between DTOs and Models in namespace namespace **BookCatalog.Services.Mapper**.

## Entity Framework Core as ORM

The database is being accessed using EF Core
Code First approach is in place with migrations located in **BookCatalog.Repositories**.

## Projects

* **BookCatalog** - the web api project
* **BookCatalog.Domain** - the data models
* **BookCatalog.Repositories** - the repositories wrappers for accessing the database
* **BookCatalog.Repositories.Tests** - the repositories wrappers NUnit tests
* **BookCatalog.Services** - the book and author services
* **BookCatalog.Services.Tests** - the services NUnit tests
