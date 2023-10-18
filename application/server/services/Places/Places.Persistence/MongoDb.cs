using MongoDB.Driver;
using Microsoft.Extensions.Options;
using MongoDB.Driver.GeoJsonObjectModel;
using Places.Application.Comments.Commands;
using Places.Application.Interfaces;
using Places.Application.Likes.CreatePlaceLike;
using Places.Application.Places.Commands.UpdatePlace;
using Places.Domain;

namespace Places.Persistence;

public class MongoDb : IMongoDb
{   
    private readonly IMongoCollection<Place> _placeCollection;
    private readonly IMongoCollection<PlaceLike> _placeLikesCollection;
    private readonly IMongoCollection<PlaceComment> _placeCommentCollection;
    private readonly IMongoCollection<ParsedPlacePhoto> _parsedPlacesPhotosCollection;
    private readonly IMongoCollection<ParsedPlacePhotoLike> _parsedPlacePhotoLikesCollection;
    private readonly IMongoCollection<ParsedPlacePhotoComment> _parsedPlacePhotoCommentsCollection;
    private readonly IMongoCollection<UploadedPlacePhoto> _uploadedPlacePhotosCollection;
    private readonly IMongoCollection<UploadedPlacePhotoLike> _uploadedPlacePhotoLikesCollection;
    private readonly IMongoCollection<UploadedPlacePhotoComment> _uploadedPlacePhotoCommentsCollection;

    public MongoDb(IOptions<MongoDatabaseSettings> databaseSettings)
    {
        MongoClient client = new MongoClient(databaseSettings.Value.ConnectionUri);
        IMongoDatabase database = client.GetDatabase(databaseSettings.Value.DatabaseName);
        _placeCollection = database.GetCollection<Place>(databaseSettings.Value.PlaceCollection);
        _placeLikesCollection = database.GetCollection<PlaceLike>(databaseSettings.Value.PlaceLikesCollection);
        _placeCommentCollection = database.GetCollection<PlaceComment>(databaseSettings.Value.PlaceCommentsCollection);
        _parsedPlacesPhotosCollection =
            database.GetCollection<ParsedPlacePhoto>(databaseSettings.Value.ParsedPlacePhotosCollection);
        _uploadedPlacePhotosCollection =
            database.GetCollection<UploadedPlacePhoto>(databaseSettings.Value.UploadedPlacePhotosCollection);
        _parsedPlacePhotoLikesCollection =
            database.GetCollection<ParsedPlacePhotoLike>(databaseSettings.Value.ParsedPlacePhotoLikesCollection);
        _uploadedPlacePhotoLikesCollection =
            database.GetCollection<UploadedPlacePhotoLike>(databaseSettings.Value.UploadedPlacePhotoLikesCollection);
        _uploadedPlacePhotoCommentsCollection =
            database.GetCollection<UploadedPlacePhotoComment>(databaseSettings.Value
                .UploadedPlacePhotoCommentsCollection);
        _parsedPlacePhotoCommentsCollection =
            database.GetCollection<ParsedPlacePhotoComment>(databaseSettings.Value.ParsedPlacePhotoCommentsCollection);
    }
    
    
    public async Task<Place> GetPlaceByName(string name,
        CancellationToken cancellationToken)
    {
        var filter = Builders<Place>.Filter.Eq(p => p.Name, name);
        var place = await _placeCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);

        return place;
    }

    public async Task<Place> GetPlaceById(string id, CancellationToken cancellationToken)
    {
        var filter =  Builders<Place>.Filter.Eq(p => p.Id, id);
        var place = await _placeCollection.Find(filter).FirstOrDefaultAsync(cancellationToken);

        return place;
    }
    
    public async Task AddOnePlaceAsync(Place place,
        CancellationToken cancellationToken)
    {
        await _placeCollection.InsertOneAsync(place, cancellationToken: cancellationToken);
    }

    public async Task AddManyPlacesAsync(IEnumerable<Place> places,
        CancellationToken cancellationToken)
    {
        await _placeCollection.InsertManyAsync(places, cancellationToken: cancellationToken);
    }
    
    public async Task AddOneParsedPlacePhoto(ParsedPlacePhoto photo,
        CancellationToken cancellationToken)
    {
        await _parsedPlacesPhotosCollection.InsertOneAsync(photo, cancellationToken: cancellationToken);
    }

    public async Task<ParsedPlacePhoto> FindParsedPlacePhoto(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<ParsedPlacePhoto>.Filter.Eq(pl => pl.PlaceId, id);

        var placePhoto = await _parsedPlacesPhotosCollection.Find(filter)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return placePhoto;
    }

    public async Task LikeParsedPlacePhoto(ParsedPlacePhotoLike parsedPlacePhotoLike)
    {
        await _parsedPlacePhotoLikesCollection.InsertOneAsync(parsedPlacePhotoLike);
    }

    public async Task CommentParsedPlacePhoto(ParsedPlacePhotoComment comment)
    {
        await _parsedPlacePhotoCommentsCollection.InsertOneAsync(comment);
    }

    public async Task AddManyParsedPlacePhotos(IEnumerable<ParsedPlacePhoto> places,
        CancellationToken cancellationToken)
    {
        await _parsedPlacesPhotosCollection.InsertManyAsync(places, cancellationToken: cancellationToken);
    }

    public async Task AddOneUploadedPlacePhoto(UploadedPlacePhoto uploadedPlacePhoto)
    {
        await _uploadedPlacePhotosCollection.InsertOneAsync(uploadedPlacePhoto);
    }

    public async Task<UploadedPlacePhoto> FindUploadedPlacePhoto(string id, CancellationToken cancellationToken)
    {
        var filter = Builders<UploadedPlacePhoto>.Filter.Eq(pl => pl.PlaceId, id);

        var placePhoto = await _uploadedPlacePhotosCollection.Find(filter)
            .FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return placePhoto;
    }

    public async Task LikeUploadedPlacePhoto(UploadedPlacePhotoLike uploadedPlacePhotoLike)
    {
        await _uploadedPlacePhotoLikesCollection.InsertOneAsync(uploadedPlacePhotoLike);
    }

    public async Task CommentUploadedPlacePhoto(UploadedPlacePhotoComment comment)
    {
        await _uploadedPlacePhotoCommentsCollection.InsertOneAsync(comment);
    }

    public async Task AddManyUploadedPlacePhotos(IEnumerable<UploadedPlacePhoto> uploadedPlacePhotos,
        CancellationToken cancellationToken)
    {
        await _uploadedPlacePhotosCollection.InsertManyAsync(uploadedPlacePhotos, cancellationToken: cancellationToken);
    }

    public async Task AddPlaceLikeAsync(PlaceLike placeLike,
        CancellationToken cancellationToken)
    {
        await _placeLikesCollection.InsertOneAsync(placeLike, cancellationToken: cancellationToken);
    }
    
    public async Task AddPlaceCommentAsync(PlaceComment placeComment,
        CancellationToken cancellationToken)
    {
        await _placeCommentCollection.InsertOneAsync(placeComment, cancellationToken: cancellationToken);
    }

    public Task<PlaceLike> FindPlaceLikeAsync(string? placeId, string? userId)
    {
        throw new NotImplementedException();
    }

    public async Task<PlaceLike> FindPlaceLikeAsync(string? placeId, string? userId,
        CancellationToken cancellationToken)
    {
        var filter = Builders<PlaceLike>.Filter.Eq(pl => pl.PlaceId, placeId) &
                     Builders<PlaceLike>.Filter.Eq(pl => pl.UserId, userId);

        var placeLikeFromDb = await _placeLikesCollection.Find(filter).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        return placeLikeFromDb;
    }

    public async Task<bool> UpdatePlaceAsync(UpdatePlaceCommand updatePlace,
        CancellationToken cancellationToken)
    {
        var placeFromDb = await _placeCollection.Find(p => p.Id == updatePlace.Id).FirstOrDefaultAsync(cancellationToken: cancellationToken);

        if (placeFromDb == null) return false;
        
        placeFromDb.Name = updatePlace.Name;
        placeFromDb.Lat = updatePlace.Lat;
        placeFromDb.Lon = updatePlace.Lon;
        placeFromDb.PlaceType = updatePlace.PlaceType;
        placeFromDb.UpdatedAt = DateTime.Now;
        
        var result = await _placeCollection.ReplaceOneAsync(p => p.Id == updatePlace.Id, placeFromDb,
            cancellationToken: cancellationToken);
        
        return result.IsAcknowledged && result.ModifiedCount > 0;
    }

    public async Task<bool> PlaceExistsAsync(string? placeId, CancellationToken cancellationToken)
    {
        var filter = Builders<Place>.Filter.Eq(p => p.Id, placeId);
        var placeExists = await _placeCollection.Find(filter).AnyAsync(cancellationToken: cancellationToken);
        return placeExists;
    }

    public async Task<IEnumerable<Place>?> FindPlacesNearbyAsync(double latitude, double longitude, int radius,
        CancellationToken cancellationToken)
    {
        try
        {
            var point = new GeoJsonPoint<GeoJson2DGeographicCoordinates>(
                new GeoJson2DGeographicCoordinates(longitude, latitude)
            );

            var filter = Builders<Place>.Filter.Near(x => x.Location, point, radius);

            var places = await _placeCollection.Find(filter).ToListAsync(cancellationToken: cancellationToken);
            return places;
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            return null;
        }
    }

    public async Task DeletePlaceLikeAsync(string placeId, string userId,
        CancellationToken cancellationToken)
    {
        var filter = Builders<PlaceLike>.Filter.Eq(pl => pl.PlaceId, placeId) &
                     Builders<PlaceLike>.Filter.Eq(pl => pl.UserId, userId);
        
        await _placeLikesCollection.DeleteOneAsync(filter, cancellationToken);
    }

    public async Task DeletePlaceAsync(string placeId,
        CancellationToken cancellationToken)
    {
        var filter = Builders<Place>.Filter.Eq(p => p.Id, placeId);

        await _placeCollection.DeleteOneAsync(filter, cancellationToken);
    }
}