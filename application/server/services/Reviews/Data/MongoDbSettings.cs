namespace Reviews.Data;

public class MongoDbSettings
{
    public string ConnectionUri { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string ReviewCollection { get; set; } = null!;
}