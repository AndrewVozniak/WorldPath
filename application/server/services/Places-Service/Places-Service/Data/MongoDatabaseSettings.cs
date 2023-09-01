namespace Places_Service.Data;

public class MongoDatabaseSettings
{
    public string ConnectionUri { get; set; } = null!;
    public string DatabaseName { get; set; } = null!;
    public string CollectionString { get; set; } = null!;
}