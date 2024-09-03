using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace AspNetIdentityEndpoints.Data;

public class AppDbContext(DbContextOptions<AppDbContext> options) : IdentityDbContext<CustomUser>(options);