using System.Security.Claims;
using AspNetIdentityEndpoints.Configuration;
using AspNetIdentityEndpoints.Data;
using AspNetIdentityEndpoints.Dtos;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddIdentityApiEndpoints<CustomUser>()
    .AddRoles<IdentityRole>()
    .AddEntityFrameworkStores<AppDbContext>();

builder.Services.AddAuthorization();

builder.Services.AddDbContext<AppDbContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

app.MapGroup("/identity")
    .MapIdentityApi<CustomUser>();      

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.MapEndpoint();
  
// app.MapGet("/public", () => $"This is a public endpoint");
//
// app.MapGet("/", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}")
//     .RequireAuthorization();  
//
// app.MapGet("/admin", (ClaimsPrincipal user) => $"Hello {user.Identity!.Name}")
//     .RequireAuthorization()
//     .RequireAuthorization(new AuthorizeAttribute() { Roles = "Admin" });
//
// app.MapPost("/logout", async (SignInManager<CustomUser> signInManager, [FromBody] object empty) =>
//     {
//         await signInManager.SignOutAsync();
//         return Results.Ok();
//     })
//     .WithOpenApi()
//     .RequireAuthorization();

app.Run();

