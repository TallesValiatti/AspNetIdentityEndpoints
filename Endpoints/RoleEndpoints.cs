using AspNetIdentityEndpoints.Configuration;
using AspNetIdentityEndpoints.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentityEndpoints.Endpoints;

public class RoleEndpoints : IEndPoint
{
    private static string _groupName = "/role";
    public void MapEndpoint(WebApplication app)
    {
        var endpointGroup = app.MapGroup(_groupName);
        endpointGroup.MapPost("/", async ([FromServices] RoleManager<IdentityRole> roleManager, CreateRole request) =>
        {
            var roleExist = await roleManager.RoleExistsAsync(request.Name);
            if(!roleExist)
            {
                await roleManager.CreateAsync(new IdentityRole(request.Name));
            }
        })
        .WithOpenApi();
    }
}