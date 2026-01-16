using CleanMinimalApi.Api.Endpoints;
using CleanMinimalApi.Infrastructure.DependencyInjection;
using Microsoft.EntityFrameworkCore;
using CleanMinimalApi.Infrastructure.Persistence;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// React will call this API, so enable CORS (adjust origin as needed)
builder.Services.AddCors(opt =>
{
    opt.AddPolicy("react", p =>
        p.WithOrigins("http://localhost:5173", "http://localhost:3000")
         .AllowAnyHeader()
         .AllowAnyMethod());
});

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseCors("react");

// Auto-migrate on startup (fine for dev, don’t blindly do this in prod)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<AppDbContext>();
    await db.Database.MigrateAsync();
}

app.MapProductEndpoints();

app.Run();
