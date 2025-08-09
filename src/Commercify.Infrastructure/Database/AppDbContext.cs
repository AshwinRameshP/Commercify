using Microsoft.EntityFrameworkCore;

namespace Commercify.Infrastructure.Database;

public class AppDbContext :DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {

    }
}
