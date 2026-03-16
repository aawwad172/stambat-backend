using Stamply.Application;
using Stamply.Application.Utilities;
using Stamply.Domain;
using Stamply.Infrastructure;
using Stamply.Infrastructure.Persistence;
using Stamply.Presentation.API;
using Stamply.Presentation.API.Endpoints;
using Stamply.Presentation.API.Middlewares;
using Stamply.Presentation.API.Models;

WebApplicationBuilder? builder = WebApplication.CreateBuilder(args);

builder.Services.AddHealthChecks()
                .AddNpgSql(builder.Configuration.GetRequiredSetting("ConnectionStrings:DbConnectionString"));

// Add services to the container.
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
// builder.Services.AddSwaggerAuth();

builder.Services.AddDomain()
                .AddApplication()
                .AddInfrastructure(builder.Configuration)
                .AddPresentation(builder.Configuration);

builder.Services.AddAuthentication();
builder.Services.AddAuthorization();

WebApplication app = builder.Build();

// Hybrid Seed Logic: Runs migrations and seeds data
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    await app.Services.ApplyMigrationsAndSeedAsync();
}

// Map health check endpoint
app.MapHealthChecks("/health");

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment() || app.Environment.IsStaging())
{
    app.UseSwagger();
    app.UseSwaggerUI(c =>
    {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stamply API v1");
        c.DocumentTitle = "Stamply API";
    });
}

app.UseAuthentication();
app.UseAuthorization();

app.UseHttpsRedirection();

app.UseMiddleware<ExceptionHandlerMiddleware>();
app.UseMiddleware<JwtMiddleware>();

app.MapGet("/", () => new
{
    message = "Welcome to My API",
    version = "1.0.0",
    links = new
    {
        self = "/",
        docs = "/swagger",
        health = "/health"
    }
}).WithTags("Home");

app.MapEndpointModules();

app.Run();
