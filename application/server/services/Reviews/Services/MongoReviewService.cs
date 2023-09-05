using Microsoft.Extensions.Options;
using MongoDB.Driver;
using Reviews.Data;
using Reviews.Models;

namespace Reviews.Services;

public class MongoReviewService
{
    private readonly IMongoCollection<Review> _reviewCollection;

    public MongoReviewService(IOptions<MongoDbSettings> databaseSettings)
    {
        MongoClient client = new MongoClient(databaseSettings.Value.ConnectionUri);
        IMongoDatabase database = client.GetDatabase(databaseSettings.Value.DatabaseName);
        _reviewCollection = database.GetCollection<Review>(databaseSettings.Value.ReviewCollection);
    }

    public async Task AddOneReviewAsync(Review review)
    {
        await _reviewCollection.InsertOneAsync(review);
    }

    public async Task<Review> GetReviewByUserId(string userId)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.UserId, userId);
        var review = await _reviewCollection.Find(filter).FirstOrDefaultAsync();

        return review;
    }

    public async Task<Review> GetReviewById(string id)
    {
        var filter = Builders<Review>.Filter.Eq(r => r.Id, id);
        var review = await _reviewCollection.Find(filter).FirstOrDefaultAsync();

        return review;
    }

    public async Task<List<Review>> GetSomeReview(int count)
    {
        var filter = Builders<Review>.Filter.Empty;
        var reviews = await _reviewCollection.Find(filter)
            .Limit(count)
            .ToListAsync();
        
        return reviews;
    }

    public async Task<List<Review>> GetAllReviews()
    {
        var reviews = await _reviewCollection.FindAsync(_ => true);
        return await reviews.ToListAsync();
    }
}