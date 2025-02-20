using Microsoft.AspNetCore.Identity;
using Module3WebApi.WebApi.Repositories;

var builder = WebApplication.CreateBuilder(args);
var sqlConnectionString = builder.Configuration["SqlConnectionString"];
var sqlConnectionStringFound = !string.IsNullOrWhiteSpace(sqlConnectionString);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

//if (string.IsNullOrWhiteSpace(sqlConnectionString))
//    throw new InvalidProgramException("Configuration variable SqlConnectionString not found");
builder.Services.AddTransient<IAuthenticationService, AspNetIdentityAuthenticationService>();


builder.Services.AddTransient<Environment2DRepository, Environment2DRepository>(o => new Environment2DRepository(sqlConnectionString));

builder.Services.AddAuthorization();

builder.Services.AddIdentityApiEndpoints<IdentityUser>(options =>
{
    options.User.RequireUniqueEmail = true;
    options.Password.RequiredLength = 15;
})
.AddRoles<IdentityRole>()
.AddDapperStores(options =>
{
    options.ConnectionString = sqlConnectionString;
});

builder.Services.AddHttpContextAccessor();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.UseAuthorization();
app.MapGroup("/account").MapIdentityApi<IdentityUser>(); 
app.MapControllers().RequireAuthorization();

//app.MapGet("/", () => $"The API is up 🚀. Connection string found {(sqlConnectionStringFound ? "✅" : "❎")}");

app.Run();
