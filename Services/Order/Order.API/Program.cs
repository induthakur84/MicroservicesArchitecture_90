using Order.Data.Services;
using RegisterDependency;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.


// only single we can inside program to register the dependency 
builder.Services.RegisterServices(typeof(UserData).Assembly.FullName);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

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
