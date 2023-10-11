using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Driver.GeoJsonObjectModel;
using Places.Application.Interfaces;
using Places.Domain;

namespace Places.Persistence;

public class MongoDb : IMongoDb
{   
    private readonly IMongoCollection<Place> _placeCollection;
    private readonly IMongoCollection<PlaceLike> _placeLikesCollection;
    private readonly IMongoCollection<PlaceComment> _placeCommentCollection;
    private readonly IMongoCollection<ParsedPlacePhoto> _parsedPlacesPhotosCollection;

    public MongoDb(IOptions<MongoDatabaseSettings> databaseSettings)
    {
        MongoClient client = new MongoClient(databaseSettings.Value.ConnectionUri);
        IMongoDatabase database = client.GetDatabase(databaseSettings.Value.DatabaseName);
        _placeCollection = database.GetCollection<Place>(databaseSettings.Value.PlaceCollection);
        _placeLikesCollection = database.GetCollection<PlaceLike>(databaseSettings.Value.PlaceLikesCollection);
        _placeCommentCollection = database.GetCollection<PlaceComment>(databaseSettings.Value.PlaceCommentsCollection);
        _parsedPlacesPhotosCollection =
            database.GetCollection<ParsedPlacePhoto>(databaseSettings.Value.ParsedPlacePhotosCollection);
    }
    
    
    public async Task<Place> GetPlaceByName(string name)
    {
        var filter = Builders<Place>.Filter.Eq(p => p.Name, name);
        var place = await _placeCollection.Find(filter).FirstOrDefaultAsync();

        return place;
    }

    public async Task<Place> GetPlaceById(string id)
    {
        var filter =  Builders<Place>.Filter.Eq(p => p.Id, id);
        var place = await _placeCollection.Find(filter).FirstOrDefaultAsync();

        return place;
    }

    public async Task AddOnePlaceAsync(Place place)
    {
        await _placeCollection.InsertOneAsync(place);
    }

    public async Task AddManyPlacesAsync(IEnumerable<Place> places)
    {
        await _placeCollection.InsertManyAsync(places);
    }

    public async Task AddOneParsedPlacePhoto(ParsedPlacePhoto photo)
    {
        await _parsedPlacesPhotosCollection.InsertOneAsync(photo);
    }

    public async Task AddManyParsedPlacePhotos(IEnumerable<ParsedPlacePhoto> places)
    {
        await _parsedPlacesPhotosCollection.InsertManyAsync(places);
    }

    public async Task AddPlaceLikeAsync(PlaceLike placeLike)
    {
        await _placeLikesCollection.InsertOneAsync(placeLike);
    }

    public async Task AddPlaceCommentAsync(PlaceComment placeComment)
    {
        await _placeCommentCollection.InsertOneAsync(placeComment);
    }

    public async Task<PlaceLike> FindPlaceLikeAsync(string? placeId, string? userId)
    {
        var filter = Builders<PlaceLike>.Filter.Eq(pl => pl.PlaceId, placeId) &
                     Builders<PlaceLike>.Filter.Eq(pl => pl.UserId, userId);

        var placeLikeFromDb = await _placeLikesCollection.Find(filter).FirstOrDefaultAsync();

        return placeLikeFromDb;
    }

    public async Task<bool> UpdatePlaceAsync(string id, Place updatePlace)
    {
        var placeFromDb = await _placeCollection.Find(p => p.Id == id).FirstOrDefaultAsync();

        if (placeFromDb == null) return false;
        
        placeFromDb.Name = updatePlace.Name;
        placeFromDb.Lat = updatePlace.Lat;
        placeFromDb.Lon = updatePlace.Lon;
        placeFromDb.PlaceType = updatePlace.PlaceType;
        placeFromDb.UpdatedAt = DateTime.Now;
        
        var result = await _placeCollection.ReplaceOneAsync(p => p.Id == id, placeFromDb);
        
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> PlaceExistsAsync(string? placeId)
    {
        var filter = Builders<Place>.Filter.Eq(p => p.Id, placeId);
        var placeExists = await _placeCollection.Find(filter).AnyAsync();
        return placeExists;
    }

    public async Task<IEnumerable<Place>?> FindPlacesNearbyAsync(double latitude, double longitude, int radius)
    {
        try
        {
            var point = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                new GeoJson2DGeographicCoordinates(longitude, latitude)
            );

            var filter = Builders<Place>.Filter.Near(x => x.Location, point, radius);

            var places = await _placeCollection.Find(filter).ToListAsync();
            return places;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task DeletePlaceLikeAsync(string placeId, string userId)
    {
        var filter = Builders<PlaceLike>.Filter.Eq(pl => pl.PlaceId, placeId) &
                     Builders<PlaceLike>.Filter.Eq(pl => pl.UserId, userId);
        
        await _placeLikesCollection.DeleteOneAsync(filter);
    }

    public async Task DeletePlaceAsync(string placeId)
    {
        var filter = Builders<Place>.Filter.Eq(p => p.Id, placeId);

        await _placeCollection.DeleteOneAsync(filter);
    }
}