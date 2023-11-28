using Create.DBContext;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
var dbHost = Environment.GetEnvironmentVariable("DB_HOST");
var dbName = Environment.GetEnvironmentVariable("DB_NAME");
var dbPassword = Environment.GetEnvironmentVariable("DB_PASSWORD");
var connectionString = $"Data Source={dbHost};Initial Catalog={dbName};User ID=root; Password={dbPassword}";
builder.Services.AddDbContext<PersonaDbContext>(opt => opt.UseMySql(connectionString, Microsoft.EntityFrameworkCore.ServerVersion.Parse("8.0.30-mysql")));
var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
