using MediatR;
using MongoDB.Bson;
using Places.Application.Interfaces;
using Places.Domain;

namespace Places.Application.Places.Commands.CreateOnePlace;

public class CreateOnePlaceCommandHandler : IRequestHandler<CreateOnePlaceCommand, Place>
{
    private readonly IMongoDb _mongoDb;

    public CreateOnePlaceCommandHandler(IMongoDb mongoDb)
    {
        _mongoDb = mongoDb;
    }

    public async Task<Place> Handle(CreateOnePlaceCommand request, CancellationToken cancellationToken)
    {
        var place = new Place
        {
            Name = request.Name,
            Lat = request.Lat,
            Lon = request.Lon,
            PlaceType = request.PlaceType,
        };

        var parsedPhoto = new ParsedPlacePhoto()
        {
            PlaceId = place.Id,
            PhotoReference = place.PhotoReference,
        };

        await _mongoDb.AddOnePlaceAsync(place, cancellationToken);
        await _mongoDb.AddOneParsedPlacePhoto(parsedPhoto, cancellationToken);

        return place;
    }
}