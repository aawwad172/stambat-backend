using Stamply.Application;
using Stamply.Application.CQRS.Commands.Authentication;
using Stamply.Application.CQRS.Commands.Tenant;
using Stamply.Application.CQRS.Queries.Authentication;
using Stamply.Application.Utilities;
using Stamply.Domain;
using Stamply.Domain.Constants;
using Stamply.Infrastructure;
using Stamply.Infrastructure.Persistence;
using Stamply.Presentation.API;
using Stamply.Presentation.API.Middlewares;
using Stamply.Presentation.API.Models;
using Stamply.Presentation.API.Routes.Authentication;
using Stamply.Presentation.API.Routes.Tenant;

using RefreshToken = Stamply.Presentation.API.Routes.Authentication.RefreshToken;

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

#region Authentication

app.MapPost(EndpointRoutes.RegisterUser, RegisterUser.RegisterRoute)
    .WithTags(EndpointTags.Authentication)
   .Produces<ApiResponse<RegisterUserCommandResult>>(StatusCodes.Status201Created, "application/json")
   .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
   .Produces<ApiResponse<RegisterUserCommandResult>>(StatusCodes.Status409Conflict, "application/json")
   .Accepts<RegisterUserCommand>("application/json");

app.MapPost(EndpointRoutes.Login, Login.RegisterRoute)
    .WithTags(EndpointTags.Authentication)
   .Produces<ApiResponse<LoginCommandResult>>(StatusCodes.Status200OK, "application/json")
   .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
   .Produces<ApiResponse<LoginCommandResult>>(StatusCodes.Status401Unauthorized, "application/json")
   .Accepts<LoginCommand>("application/json");

app.MapPost(EndpointRoutes.RefreshToken, RefreshToken.RegisterRoute)
    .WithTags(EndpointTags.Authentication)
    .RequireAuthorization("UserRead")
   .Produces<ApiResponse<RefreshTokenCommandResult>>(StatusCodes.Status200OK, "application/json")
   .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
   .Produces<ApiResponse<RefreshTokenCommandResult>>(StatusCodes.Status401Unauthorized, "application/json")
   .Accepts<RefreshTokenCommand>("application/json");

app.MapPost(EndpointRoutes.Logout, Logout.RegisterRoute)
    .WithTags(EndpointTags.Authentication)
    .RequireAuthorization("UserRead", "PostApprove")
   .Produces<ApiResponse<LogoutCommandResult>>(StatusCodes.Status200OK, "application/json")
   .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
   .Produces<ApiResponse<LogoutCommandResult>>(StatusCodes.Status401Unauthorized, "application/json")
   .Accepts<LogoutCommand>("application/json");

app.MapPost(EndpointRoutes.VerifyEmail, VerifyEmail.RegisterRoute)
    .WithTags(EndpointTags.Authentication)
    .Accepts<VerifyEmailCommand>("application/json")
    .Produces<ApiResponse<VerifyEmailCommandResult>>(StatusCodes.Status200OK);

app.MapGet(EndpointRoutes.IsVerified, IsUserVerified.RegisterRoute)
    .WithTags(EndpointTags.Authentication)
    .Produces<ApiResponse<IsUserVerifiedQueryResult>>(StatusCodes.Status200OK, "application/json")
    .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
    .Accepts<IsUserVerifiedQuery>("application/json")
    .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json");
#endregion

#region Tenant
app.MapPost(EndpointRoutes.InviteTenant, InviteTenant.RegisterRoute)
    .WithTags(EndpointTags.Tenant)
    .RequireAuthorization("SuperAdmin.InviteTenant")
    .Produces<ApiResponse<InviteTenantCommandResult>>(StatusCodes.Status200OK, "application/json")
    .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
    .Accepts<InviteTenantCommand>("application/json");

app.MapPost(EndpointRoutes.InviteMerchant, InviteMerchant.RegisterRoute)
    .WithTags(EndpointTags.Tenant)
    .RequireAuthorization("Tenant.InviteUser") // Policy checks if User belongs to this Tenant
    .Produces<ApiResponse<InviteMerchantCommandResult>>(StatusCodes.Status200OK, "application/json")
    .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json")
    .Accepts<InviteMerchantCommand>("application/json");

// Todo: Add some permission for this endpoint
app.MapPost(EndpointRoutes.SetupTenant, SetupTenant.RegisterRoute)
    .WithTags(EndpointTags.Tenant)
    .Produces<ApiResponse<SetupTenantCommandResult>>(StatusCodes.Status200OK, "application/json")
    .Produces<ApiResponse<IEnumerable<string>>>(StatusCodes.Status400BadRequest, "application/json");
#endregion

app.Run();
