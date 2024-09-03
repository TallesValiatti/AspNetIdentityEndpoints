using AspNetIdentityEndpoints.Configuration;
using AspNetIdentityEndpoints.Data;
using AspNetIdentityEndpoints.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentityEndpoints.Endpoints;

public class LogoutEndpoints : IEndPoint
{
    public void MapEndpoint(WebApplication app)
    {
            app.MapPost("/logout", async (SignInManager<CustomUser> signInManager, [FromBody] object empty) =>
        {
            await signInManager.SignOutAsync();
            return Results.Ok();
        })
        .WithOpenApi()
        .RequireAuthorization();
    }
}