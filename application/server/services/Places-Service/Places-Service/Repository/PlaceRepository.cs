using Microsoft.EntityFrameworkCore;
using Places_Service.Data;
using Places_Service.Models;
using Places_Service.Repository.Interfaces;

namespace Places_Service.Repository;

public class PlaceRepository : IPlaceRepository
{
    private readonly AppDbContext _context;

    public PlaceRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Place?> GetPlaceByCoordinate(float lat, float lon)
    {
        var tolerance = 0.001;

        return await _context.Places
            .FirstOrDefaultAsync(p => p != null && Math.Abs(p.Lat - lat) < tolerance && Math.Abs(p.Lon - lon) < tolerance);
    }

    public async Task<Place?> GetPlacesById(int id) => await _context.Places.FindAsync(id);

    public async Task<Place?> GetPlaceByName(string name) => await _context.Places
        .FirstOrDefaultAsync(p => p != null && p.Name == name);

    public async Task SaveAsync()
    {
        await _context.SaveChangesAsync();
    }
}