using LibraryManagementAPI.Data;
using LibraryManagementAPI.Mappings;
using LibraryManagementAPI.Middlewares;
using LibraryManagementAPI.Models;
using LibraryManagementAPI.Services;
using LibraryManagementAPI.Services.AuthorService;
using LibraryManagementAPI.Services.BookService;
using Microsoft.EntityFrameworkCore;
using Serilog;

var builder = WebApplication.CreateBuilder(args);


//add Serilog -logging
var logger = new LoggerConfiguration()
    .WriteTo.Console()
    .WriteTo.File("Logs/Library_Log.txt", rollingInterval: RollingInterval.Day)
    .MinimumLevel.Information()
    .CreateLogger();

builder.Logging.ClearProviders();
builder.Logging.AddSerilog(logger);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

//Inject DbContext Class
builder.Services.AddDbContext<LibraryManagementDbContext>(options => options.UseNpgsql(builder.Configuration.GetConnectionString("PostgreSQL")));

builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddScoped<ILibraryService, LibraryService>();


//Add auto-mapper for DTOs
builder.Services.AddAutoMapper(typeof(AutoMapperProfiles));



var app = builder.Build();


// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseMiddleware<ExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

