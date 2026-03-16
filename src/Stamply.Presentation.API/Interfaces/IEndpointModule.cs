namespace Stamply.Presentation.API.Interfaces;

/// <summary>
/// Defines a contract for modular route registration in Minimal APIs.
/// </summary>
public interface IEndpointModule
{
    /// <summary>
    /// Maps the endpoints for this specific module.
    /// </summary>
    /// <param name="app">The route builder for the application.</param>
    void MapEndpoints(IEndpointRouteBuilder app);
}
