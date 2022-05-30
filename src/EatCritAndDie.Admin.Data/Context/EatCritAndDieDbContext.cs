using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EatCritAndDie.Admin.Data.Context;

public class EatCritAndDieDbContext : IdentityDbContext
{
    public EatCritAndDieDbContext(DbContextOptions<EatCritAndDieDbContext> options)
        : base(options)
    {
    }
}