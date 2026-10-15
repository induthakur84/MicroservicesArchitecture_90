using Microsoft.EntityFrameworkCore;
using MigrationUpdate;
using Order.Data.Context;
using Order.Data.Services;
using RegisterDependency;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<StoreDbContext>(options =>
options.UseSqlServer(builder.Configuration.GetConnectionString("DbConnection")));

// only single we can inside program to register the dependency 
builder.Services.RegisterServices(typeof(UserData).Assembly.FullName);

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();



// changes for update-databas


var deployDatabaseChanges = app.Configuration.GetValue<bool>("DeployDatabaseChanges");

if (deployDatabaseChanges)
{

    MigrationManager.UpdateDatabase<StoreDbContext>(app);
}



// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
