using Microsoft.AspNetCore.Mvc.Infrastructure;
using Salon.API;
using Salon.API.Errors;
using Salon.Application;
using Salon.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services
    .AddPresentation()
    .AddApplication()
    .AddInfrastructure(builder.Configuration);



var app = builder.Build();

app.UseExceptionHandler("/error");
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
