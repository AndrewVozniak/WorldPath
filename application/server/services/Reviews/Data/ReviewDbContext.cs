using Microsoft.EntityFrameworkCore;
using Reviews.Models;

namespace Reviews.Data;

public class ReviewDbContext : DbContext
{
    public ReviewDbContext(DbContextOptions<ReviewDbContext> options) : base(options)
    {
        
    }
    
    public DbSet<Review> Reviews { get; set; }
}