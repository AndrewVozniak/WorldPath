using Microsoft.CodeAnalysis.Options;
using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Places_Service.Data;
using Places_Service.Models;
using MongoDatabaseSettings = Places_Service.Data.MongoDatabaseSettings;

namespace Places_Service.Services;

public class PlaceService
{
    private readonly IMongoCollection<Place> _placeCollection;

    public PlaceService(IOptions<MongoDatabaseSettings> databaseSettings)
    {
        MongoClient client = new MongoClient(databaseSettings.Value.ConnectionUri);
        IMongoDatabase database = client.GetDatabase(databaseSettings.Value.DatabaseName);
        _placeCollection = database.GetCollection<Place>(databaseSettings.Value.CollectionString);
    }

    public async Task CreateManyAsync(List<Place> places)
    {
        await _placeCollection.InsertManyAsync(places);
    }
}