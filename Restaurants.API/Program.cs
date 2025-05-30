// In this file we'll be able to specify how our API should work
// What dependencies are we going to register in the built in dependency container, and also how the http pipeline will look like



using Microsoft.AspNetCore.Identity;
using Microsoft.OpenApi.Models;
using Restaurants.API.Extensions;
using Restaurants.API.Middlewares;
using Restaurants.Application.Extensions;
using Restaurants.Domain.Entities;
using Restaurants.Infrastructure.Extensions;
using Restaurants.Infrastructure.Seeders;
using Serilog;
using Serilog.Events;

var builder = WebApplication.CreateBuilder(args);


// Add services to the container.

// Here we are able to register some dependencies into the container
builder.Configuration.GetConnectionString("RestaurantDb");

builder.AddPresentation();
builder.Services.AddApplication();

builder.Services.AddInfrastructure(builder.Configuration);





var app = builder.Build();


var scope = app.Services.CreateScope();
var seeder = scope.ServiceProvider.GetRequiredService<IRestaurantSeeder>();
await seeder.Seed();

// Configure the HTTP request pipeline.

// Middleware

app.UseMiddleware<ErrorHandlingMiddleware>();
app.UseMiddleware<RequestTimeLoggingMiddleware>();

app.UseSerilogRequestLogging();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();

}

app.UseHttpsRedirection();

app.MapGroup("api/identity")
    .WithTags("Identity")
    .MapIdentityApi<User>();

app.UseAuthorization();

app.MapControllers(); // <-- Specify which route from the http request path will be bind to what action in our controller

app.Run();
