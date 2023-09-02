﻿using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using MongoDB.Driver.GeoJsonObjectModel;
using Places_Service.Data;
using Places_Service.Models;
using MongoDatabaseSettings = Places_Service.Data.MongoDatabaseSettings;

namespace Places_Service.Services;

public class PlaceService
{
    private readonly IMongoCollection<Place> _placeCollection;
    private readonly IMongoCollection<PlaceLike> _placeLikesCollection;
    private readonly IMongoCollection<PlaceComment> _placeCommentCollection;

    public PlaceService(IOptions<MongoDatabaseSettings> databaseSettings)
    {
        MongoClient client = new MongoClient(databaseSettings.Value.ConnectionUri);
        IMongoDatabase database = client.GetDatabase(databaseSettings.Value.DatabaseName);
        _placeCollection = database.GetCollection<Place>(databaseSettings.Value.PlaceCollection);
        _placeLikesCollection = database.GetCollection<PlaceLike>(databaseSettings.Value.PlaceLikesCollection);
        _placeCommentCollection = database.GetCollection<PlaceComment>(databaseSettings.Value.PlaceCommentsCollection);
    }

    public async Task AddManyPlacesAsync(IEnumerable<Place> places)
    {
        await _placeCollection.InsertManyAsync(places);
    }
    
    public async Task AddPlaceLikeAsync(PlaceLike placeLike)
    {
        await _placeLikesCollection.InsertOneAsync(placeLike);
    }
    
    public async Task<PlaceLike> FindPlaceLikeAsync(string? placeId, string? userId)
    {
        var filter = Builders<PlaceLike>.Filter.Eq(pl => pl.PlaceId, placeId) &
                     Builders<PlaceLike>.Filter.Eq(pl => pl.UserId, userId);

        var placeLikeFromDb = await _placeLikesCollection.Find(filter).FirstOrDefaultAsync();

        return placeLikeFromDb;
    }
    
    public async Task<bool> PlaceExistsAsync(string? placeId)
    {
        var filter = Builders<Place>.Filter.Eq(p => p.Id, placeId);
        var placeExists = await _placeCollection.Find(filter).AnyAsync();
        return placeExists;
    }
    
    public async Task<IEnumerable<Place>> FindPlacesNearbyAsync(double latitude, double longitude)
    {
        var point = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
            new GeoJson2DGeographicCoordinates(longitude, latitude)
        );

        var filter = Builders<Place>.Filter.Near(x => x.Location, point, 1000);

        var places = await _placeCollection.Find(filter).ToListAsync();
        return places;
    }
    
    public async Task DeletePlaceLikeAsync(string placeId, string userId)
    {
        var filter = Builders<PlaceLike>.Filter.Eq(pl => pl.PlaceId, placeId) &
                     Builders<PlaceLike>.Filter.Eq(pl => pl.UserId, userId);
        
        await _placeLikesCollection.DeleteOneAsync(filter);
    }
}