using ApplicationCore.Commons.Repository;
using ApplicationCore.Models;
using ApplicationCore.Models.QuizAggregate;
using BackendLab01;
using Infrastructure.Memory;
using Infrastructure.Memory.Generators;
using Infrastructure.Memory.Repositories;

var builder = WebApplication.CreateBuilder(args);

// Swagger services
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddSingleton<IGenericRepository<Quiz, int>, MemoryGenericRepository<Quiz, int>>();
builder.Services.AddSingleton<IGenericRepository<QuizItem, int>, MemoryGenericRepository<QuizItem, int>>();
builder.Services.AddSingleton<IGenericRepository<QuizItemUserAnswer, string>, MemoryGenericRepository<QuizItemUserAnswer, string>>();
builder.Services.AddSingleton<IQuizAdminService, QuizAdminService>();
builder.Services.AddSingleton<IQuizUserService, QuizUserService>();
builder.Services.AddTransient<IGenericGenerator<int>, IntGenerator>();


var app = builder.Build();

// Swagger middleware
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();
app.MapControllers();

// Example endpoint (you can add more later)
app.MapGet("/", () => Results.Ok("Hello API!"));

app.Seed();
app.Run();