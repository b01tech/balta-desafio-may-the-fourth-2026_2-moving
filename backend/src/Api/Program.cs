using Api.Extensions;
using Infrastructure.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddMovingServices(builder.Configuration);
builder.Services.AddMovingCors();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    var context = scope.ServiceProvider.GetRequiredService<MovingDbContext>();
    context.Database.EnsureCreated();

    if (!context.Caixas.Any())
    {
        var seedData = SeedData.GetCaixas();
        context.Caixas.AddRange(seedData);
        context.SaveChanges();
    }
}

app.UseCors("AllowFrontend");

app.MapMovingEndpoints();

app.Run();
