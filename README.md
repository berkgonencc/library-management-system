# Library Management System
This is a basic library management system implemented in C# that allows you to manage a collection of books and perform various operations such as checking out books, retrieving books by author, and getting a list of checked-out books.

### Technologies Used:
C#, ASP.Net Core WebAPI, Docker, PostgreSQL, DBeaver, LINQ, EFCore.

## Prerequisites
Before you get started, ensure you have the following tools and software installed on your system:

- [.NET Core SDK](https://dotnet.microsoft.com/download/dotnet)
- [Docker](https://www.docker.com/get-started)
- [PostgreSQL](https://www.postgresql.org/download/)
- [DBeaver](https://dbeaver.io/download/)

## Getting Started
To get started with the Library Management System, follow these steps:
1. Clone the repository:
   ```bash
   git clone https://github.com/berkgonencc/library-management-system.git

2. Build the Docker container:
    ```bash
    docker run --name PostgreSQL -p 5432:5432 -e POSTGRES_PASSWORD=your-password -d postgres
    ```
3. Install dotnet ef global tools
   ```bash
   dotnet tool install --global dotnet-ef
   ```
4. Database Setup:
   - Create a PostgreSQL database. You can use DBeaver or any other PostgreSQL management tool.
   - Update the database connection string in the appsettings.json file:
    ```bash
      "ConnectionStrings": {
    "PostgreSQL": "User ID=postgres;Password=your-password;Host=localhost;Port=5432;Database=LibraryAPIDb;"
    }
    ```
5. Database Migrations:
   - Create the initial migration to generate the database schema:
   ```bash
   dotnet ef migrations add InitialCreate
   ```
   - Apply the initial migration to create the database schema:
   ```bash
   dotnet ef database update
   ```
6. Build and run the application:
    ```bash
    dotnet restore
    dotnet build
    dotnet run
    ```
7. Use Swagger or any API testing tool to interact with the API endpoints.

8. Run the unit tests to verify the functionality of the Library class.

## API Endpoints
The API provides the following endpoints:

* GET /api/authors: Get a list of all authors.
* POST /api/authors: Create a new author.
* DELETE /api/authors/{id}: Delete the author.
* POST /api/books: Create a new book.
* GET /api/library: Get a list of all books.
* GET /api/library/books-by-author: Get books by a specific author.
* GET /api/library/checked-out-books: Get all checked-out books.
* PUT /api/library/checkout/{isbn}: Change the CheckedOut status of the book.

## Testing
Unit tests are included in the project. You can run them using a testing framework like xUnit. 
Use the following command to run the tests:
   ```bash
    dotnet test
   ```
    


