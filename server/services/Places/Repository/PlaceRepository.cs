using Microsoft.EntityFrameworkCore;
using Places.Data;
using Places.Models;
using Places.Repository.Interface;

namespace Places.Repository;

public class PlaceRepository : IPlaceRepository
{
    private readonly PlaceDbContext _context;
    
    public PlaceRepository(PlaceDbContext context)
    {
        _context = context;
    }

    public Task<List<Place>> GetPlacesAsync() => _context.Places.ToListAsync();

    public async Task<Place?> GetPlaceByIdAsync(int goodId) 
        => await _context.Places.FindAsync(new object[] { goodId });

    public async Task InsertPlacesAsync(Place place) => await _context.Places.AddAsync(place);
         
    public async Task UpdatePlacesAsync(Place place)
    {
        var placeFromDb = await _context.Places.FindAsync(new object[] { place.Id });
        if (placeFromDb == null) return;
        placeFromDb.Name = place.Name;
        placeFromDb.Lat = place.Lat;
        placeFromDb.Lon = place.Lon;
    }
    public async Task DeletePlacesAsync(int goodId)
    {
        var goodFromDb = await _context.Places.FindAsync(new object[] { goodId });
        if (goodFromDb == null) return;
        _context.Places.Remove(goodFromDb);
    }

    public async Task SaveAsync() => await _context.SaveChangesAsync();

    private bool _disposed = false;
    protected virtual void Dispose(bool disposing)
    {
        if (!_disposed)
        {
            if (disposing)
            {
                _context.Dispose();
            }
            _disposed = true;
        }
    }
    public void Dispose()
    {
        Dispose(true);
        GC.SuppressFinalize(this);
    }
}
