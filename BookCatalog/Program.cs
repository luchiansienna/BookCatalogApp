using BookCatalog.MiddlewareHandlers;
using BookCatalog.Repositories;
using BookCatalog.Services;
using BookCatalog.Services.Contracts;
using BookCatalog.Services.Mapper;
using BookCatalog.Services.UnitOfWork;
using Microsoft.EntityFrameworkCore;
using System.Text.Json.Serialization;
using FluentValidation;
using FluentValidation.AspNetCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<BookCatalogContext>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("BookCatalogContext"));
});

builder.Services.AddControllers().AddJsonOptions(x =>
               { x.JsonSerializerOptions.ReferenceHandler = ReferenceHandler.IgnoreCycles; });
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


builder.Services.AddScoped<IBookServices, BookServices>();
builder.Services.AddScoped<IAuthorServices, AuthorServices>();
builder.Services.AddScoped<IRepositoriesWrapper, RepositoriesWrapper>();

builder.Services.AddAutoMapper(typeof(MappingProfiles));
builder.Services.AddFluentValidationAutoValidation();
builder.Services.AddValidatorsFromAssembly(typeof(Program).Assembly);


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}


app.UseMiddleware<GlobalErrorHandler>();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
