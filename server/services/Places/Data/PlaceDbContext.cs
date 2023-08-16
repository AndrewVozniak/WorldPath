using Microsoft.EntityFrameworkCore;
using Places.Models;

namespace Places.Data;

public class PlaceDbContext : DbContext
{
    public PlaceDbContext(DbContextOptions<PlaceDbContext> options) : base(options)
    {
        
    }

    public DbSet<Place> Places { get; set; }
}