using Microsoft.EntityFrameworkCore;
using Places_Service.Models;

namespace Places_Service.Data;

public class AppDbContext : DbContext
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
        
    }

    public DbSet<Place> Places { get; set; }
    public DbSet<PlaceComment> PlaceComments { get; set; }
    public DbSet<PlaceLike> PlaceLikes { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        modelBuilder.Entity<PlaceComment>()
            .HasOne(pc => pc.Place)
            .WithMany(p => p.Comments)
            .HasForeignKey(pc => pc.PlaceId);

        modelBuilder.Entity<PlaceLike>()
            .HasOne(pl => pl.Place)
            .WithMany(p => p.Likes)
            .HasForeignKey(pl => pl.PlaceId);
    }
}