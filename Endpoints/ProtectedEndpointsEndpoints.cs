using System.Security.Claims;
using AspNetIdentityEndpoints.Configuration;
using Microsoft.AspNetCore.Authorization;

namespace AspNetIdentityEndpoints.Endpoints;

public class ProtectedEndpoints : IEndPoint
{
    private const string GroupName = "/protected";

    public void MapEndpoint(WebApplication app)
    {
        var endpointGroup = app.MapGroup(GroupName);
            
        endpointGroup.MapPost("/", (ClaimsPrincipal user) =>
        {
            var name = user.Identity!.Name;
            var isUser = user.IsInRole("User");
            var isAdmin = user.IsInRole("Admin");
            
            return Results.Ok();
        })
        .WithOpenApi()
        .RequireAuthorization();
        
        endpointGroup.MapPost("/admin-role", (ClaimsPrincipal user) =>
        {
            var name = user.Identity!.Name;
            var isAdmin = user.IsInRole("Admin");
            
            return Results.Ok();
        })
        .WithOpenApi()
        .RequireAuthorization(new AuthorizeAttribute() { Roles = "Admin" });
    }
}