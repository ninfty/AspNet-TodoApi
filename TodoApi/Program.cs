using Microsoft.EntityFrameworkCore;
using TodoApi.AutoMapper;
using TodoApi.Database;
using TodoApi.Models;
using TodoApi.Repositories;
using TodoApi.Services;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using System.Configuration;
using TodoApi.Utils;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

//builder.Services.AddDbContext<AppDbContext>(options =>
//    options.UseInMemoryDatabase("DBConnection"));

builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("TodoDB")));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddScoped<ITodoService, TodoService>();
builder.Services.AddScoped<IAuthService, AuthService>();
builder.Services.AddScoped<ITodoRepository, TodoRepository>();
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddSingleton<TokenProvider>();
builder.Services.AddSingleton<PasswordHasher>();

builder.Services.AddAutoMapper(typeof(ConfigurationMapping));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();

public partial class Program { }