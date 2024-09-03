using Microsoft.AspNetCore.Identity;

namespace AspNetIdentityEndpoints.Data;

public class CustomUser : IdentityUser   
{
    public string? PersonalIdentifier { get; set; }
}