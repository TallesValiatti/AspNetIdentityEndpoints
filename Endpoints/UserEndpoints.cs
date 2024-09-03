
using AspNetIdentityEndpoints.Configuration;
using AspNetIdentityEndpoints.Data;
using AspNetIdentityEndpoints.Dtos;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace AspNetIdentityEndpoints.Endpoints;

public class UserEndpoints : IEndPoint
{
    private static string _groupName = "/user";
    public void MapEndpoint(WebApplication app)
    {
        var endpointGroup = app.MapGroup(_groupName);
        endpointGroup.MapPost("/", async (
                [FromServices] IUserStore<CustomUser> userStore,
                [FromServices] SignInManager<CustomUser> signInManager,
                [FromServices] UserManager<CustomUser> userManager, 
                [FromServices] RoleManager<IdentityRole> roleManager,  
                CreateUser request) =>
        {
            if (!await roleManager.RoleExistsAsync(request.Role))
            {
                return Results.NotFound("Role not found");
            }
            
            var user = await userManager.FindByEmailAsync(request.Email);
            if (user == null)
            {
                user = new CustomUser
                {
                    PersonalIdentifier = request.PersonalIdentifier,
                    UserName = request.Email,
                    Email = request.Email,
                    EmailConfirmed = true
                };

                var result = await userManager.CreateAsync(user, request.Password);
                
                if (!result.Succeeded)
                {
                    return Results.BadRequest("Unable to create the user");
                } 
                
                await userManager.AddToRoleAsync(user, request.Role);
            }

            return Results.Ok();
        })
        .WithOpenApi();
    }
}