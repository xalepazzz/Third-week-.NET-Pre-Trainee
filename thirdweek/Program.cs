using Swashbuckle.AspNetCore;
using DatabaseLayer;
using DatabaseLayer.Interfaces;
using BuisnessLogic.Interfaces;
using BuisnessLogic;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IAuthorRepository, AuthorRepository>();
builder.Services.AddSingleton<IBookRepository, BookRepository>();
builder.Services.AddScoped<IAuthorService, AuthorService>();
builder.Services.AddScoped<IBookService, BookService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();