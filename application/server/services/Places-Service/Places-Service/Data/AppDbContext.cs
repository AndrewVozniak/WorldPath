using Microsoft.EntityFrameworkCore;
using Places_Service.Models;

namespace Places_Service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Place> Places { get; set; }
}