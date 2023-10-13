﻿using MediatR;
using Places.Application.Interfaces;
using Places.Application.Places.Commands.CreateOnePlace;
using Places.Domain;

namespace Places.Application.Places.Commands.CreateManyPlaces;

public class CreatePlacesCommandHandler : IRequestHandler<CreateManyPlacesCommand, List<Place>>
{
    private readonly IMongoDb _mongoDb;

    public CreatePlacesCommandHandler(IMongoDb mongoDb)
    {
        _mongoDb = mongoDb;
    }

    public async Task<List<Place>> Handle(CreateManyPlacesCommand request, CancellationToken cancellationToken)
    {
        var newPlaces = new List<Place>();
        var parsedPhotos = new List<ParsedPlacePhoto>();

        foreach (var result in request.Results)
        {
            var placeType = string.Join(",", result.Types);

            var newPlace = new Place
            {
                Name = result.Name,
                Lat = result.Geometry.Location.Lat,
                Lon = result.Geometry.Location.Lng,
                PlaceType = placeType,
                CreatedAt = DateTime.Now
            };

            newPlaces.Add(newPlace);

            // Перевірка наявності фотографій і ініціалізація parsedPhotos за замовчуванням, якщо фотографій немає
            if (result.Photos != null)
            {
                foreach (var photo in result.Photos)
                {
                    var parsedPhoto = new ParsedPlacePhoto()
                    {
                        PlaceId = newPlace.Id,
                        PhotoPath = photo.PhotoReference // Або інший спосіб отримання шляху до фотографії
                    };
                    parsedPhotos.Add(parsedPhoto); // Додавання об'єкта ParsedPlacePhoto в список
                }
            }
        }
        // parsedPhotos тепер завжди існує і може бути пустим списком, якщо фотографій не було
        
        await _mongoDb.AddManyPlacesAsync(newPlaces, cancellationToken);
        await _mongoDb.AddManyParsedPlacePhotos(parsedPhotos, cancellationToken);

        return newPlaces;
    }
}