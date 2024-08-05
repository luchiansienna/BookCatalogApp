# Exercise
Create an application to manage a catalog of books. Each book will have a list of authors (that can be empty). At least the application must save the following information:
* Author: name, surname, birthyear
* Book: title, author list, publisher, edition, published date

## Functionality
The app must expose a small set of REST endpoints to store and retrieve information in JSON format. These endpoints are:
* http://locatlhost:8080/author
* http://localhost:8008/author/{author_id}
* http://localhost:8080/book 
* http://localhost:8080/book/{book_id}

Root endpoints (i.e. /author & /book) must have the following operations:
* GET - retrieve a list of summarized information
* POST - create a new element

Particular endpoints (i.e. /author/{author_id} & /book/{book_id}) must have the following operations:
* GET - retrieve full information for a particular id
* PUT - update the information for a particular id
* DELETE - remove an element by its id

**Important note**: do not allow the removal of an author when at least one book is related to it.

## Data Storage
You are free to decide where to store information. For the goal of this exercise, it is valid to use in-memory / volatile storage. 
