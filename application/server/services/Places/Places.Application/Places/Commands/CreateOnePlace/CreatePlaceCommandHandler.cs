using MediatR;
using MongoDB.Bson;
using Places.Application.Interfaces;
using Places.Domain;

namespace Places.Application.Places.Commands.CreateOnePlace;

public class CreatePlaceCommandHandler : IRequestHandler<CreateOnePlaceCommand, Place>
{
    private readonly IMongoDb _mongoDb;

    public CreatePlaceCommandHandler(IMongoDb mongoDb)
    {
        _mongoDb = mongoDb;
    }

    public async Task<Place> Handle(CreateOnePlaceCommand request, CancellationToken cancellationToken)
    {
        var place = new Place
        {
            Id = ObjectId.GenerateNewId().ToString(),
            Name = request.Name,
            Lat = request.Lat,
            Lon = request.Lon,
            PlaceType = request.PlaceType,
            CreatedAt = DateTime.Now,
            PhotoReference = request.PhotoReference
        };

        var parsedPhoto = new ParsedPlacePhoto()
        {
            PlaceId = place.Id,
            PhotoPath = place.PhotoReference,
            CreatedAt = DateTime.Now
        };

        await _mongoDb.AddOnePlaceAsync(place, cancellationToken);
        await _mongoDb.AddOneParsedPlacePhoto(parsedPhoto, cancellationToken);

        return place;
    }
}