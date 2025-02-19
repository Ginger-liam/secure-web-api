using Module3WebApi.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
var sqlConnectionString = builder.Configuration["SqlConnectionString"];

Console.WriteLine($"SQL Connection String: {sqlConnectionString}");


// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

if (string.IsNullOrWhiteSpace(sqlConnectionString))
    throw new InvalidProgramException("Configuration variable SqlConnectionString not found");

builder.Services.AddTransient<WeatherForecastRepository, WeatherForecastRepository>(o => new WeatherForecastRepository(sqlConnectionString));
builder.Services.AddTransient<Environment2DRepository, Environment2DRepository>(o => new Environment2DRepository(sqlConnectionString));

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
