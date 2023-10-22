# Library Management System
This is a basic library management system implemented in C# that allows you to manage a collection of books and perform various operations such as checking out books, retrieving books by author, and getting a list of checked out books.

## Technologies Used:
C# .Net, ASP.Net Core WebAPI, Docker, PostgreSQL, DBeaver.

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
    "ConnectionStrings": {
  "DefaultConnection": "Host=your-postgresql-host;Database=librarydb;Username=your-username;Password=your-password"}

3. Build and run the application:
    ```bash
    dotnet restore
    dotnet build
    dotnet run

4. Use Swagger or any API testing tool to interact with the API endpoints.

5. Run the unit tests to verify the functionality of the Library class.

## Docker Support
This project is Docker-ready. If you prefer running the application in a Docker container, follow these steps:
1. Build the Docker image:
    ```bash
    docker build -t library-management-system .

2. Run the Docker container:
    ```bash
    docker run -d -p 8080:80 library-management-system
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

    


