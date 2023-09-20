using System.Text;
using Places_Service.Controllers;
using RabbitMQ.Client;
using RabbitMQ.Client.Events;

namespace Places_Service.Async;

public class RabbitMqMessageHandler
{
    private readonly IConfiguration _configuration;
    private readonly PlacesController _placesController;
    private readonly IModel _rabbitMqChannel;
    private readonly IConnection _connection;

    public RabbitMqMessageHandler(PlacesController placesController, IModel rabbitMqChannel, IConfiguration configuration)
    {
        _placesController = placesController;
        _rabbitMqChannel = rabbitMqChannel;
        _configuration = configuration;

        var factory = new ConnectionFactory()
        {
            HostName = _configuration["RabbitMQHost"],
            Port = int.Parse(_configuration["RabbitMQPort"]!)
        };
        
        _connection = factory.CreateConnection();
        _rabbitMqChannel = _connection.CreateModel();
    }
    
    public void HandleMessages()
    {
        _rabbitMqChannel.QueueDeclare("getPlace_byName", false, false, false, null);
        _rabbitMqChannel.QueueDeclare("getPlace_byId", false, false, false, null);
    }
}