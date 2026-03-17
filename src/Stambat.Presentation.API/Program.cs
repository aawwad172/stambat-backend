using Stambat.Application;
using Stambat.Application.Utilities;
using Stambat.Domain;
using Stambat.Infrastructure;
using Stambat.Infrastructure.Persistence;
using Stambat.WebAPI;
using Stambat.WebAPI.Endpoints;
using Stambat.WebAPI.Middlewares;

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
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Stambat API v1");
        c.DocumentTitle = "Stambat API";
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
