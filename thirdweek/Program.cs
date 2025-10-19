using Swashbuckle.AspNetCore;
using System.Text.Json.Serialization;
using thirdweek;

var builder = WebApplication.CreateBuilder(args);
builder.Services.AddControllers();
builder.Services.AddSingleton<IAuthorRepository, AuthorRepository>();
builder.Services.AddSingleton<IBookRepository, BookRepository>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
var app = builder.Build();

app.UseRouting();
app.MapControllers();
app.UseSwagger();
app.UseSwaggerUI();

app.Run();
