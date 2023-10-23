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
   git clone https://github.com/yourusername/library-management-system.git

2. Database Setup:
   - Create a PostgreSQL database. You can use DBeaver or any other PostgreSQL management tool.
   - Update the database connection string in the appsettings.json file:
    ```bash
    "ConnectionStrings": {"DefaultConnection": "Host=your-postgresql-host;Database=librarydb;Username=your-username;Password=your-password"}
    ```
3. Database Migrations:
   - Create the initial migration to generate the database schema:
   ```bash
   dotnet ef migrations add InitialCreate
   ```
   - Apply the initial migration to create the database schema:
   ```bash
   dotnet ef database update
   ```
4. Build and run the application:
    ```bash
    dotnet restore
    dotnet build
    dotnet run
    ```
5. Use Swagger or any API testing tool to interact with the API endpoints.

6. Run the unit tests to verify the functionality of the Library class.

## Docker Support
This project is Docker-ready. If you prefer running the application in a Docker container, follow these steps:
1. Build the Docker container:
    ```bash
    docker run --name PostgreSQL -p 5432:5432 -e POSTGRES_PASSWORD=031506 -d postgres | User name = postgres
    ```
    
The application will be available at http://localhost:8080.

## API Endpoints
The API provides the following endpoints:

* GET /api/authors: Get a list of all authors.
* POST /api/books: Create a new book.
* POST /api/authors: Create a new author.
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
    


