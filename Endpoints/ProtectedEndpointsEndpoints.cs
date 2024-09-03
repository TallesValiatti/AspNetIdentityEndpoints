using System.Security.Claims;
using AspNetIdentityEndpoints.Configuration;
using AspNetIdentityEndpoints.Data;
using AspNetIdentityEndpoints.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentityEndpoints.Endpoints;

public class ProtectedEndpoints : IEndPoint
{
    private static string _groupName = "/protected";
    public void MapEndpoint(WebApplication app)
    {
        var endpointGroup = app.MapGroup(_groupName);
            
        endpointGroup.MapPost("/", (ClaimsPrincipal user) =>
        {
            var name = user.Identity!.Name;
            
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