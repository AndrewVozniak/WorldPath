using MediatR;
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

        for (var i = 0; i < request.Results.Count; i++)
        {
                    // Get place from placeData
            var result = request.Results[i];
                    
            var placeType = string.Join(",", result.Types);
                    
            var newPlace = new Place
            {
                Name = result.Name,
                Lat = result.Geometry.Location.Lat,
                Lon = result.Geometry.Location.Lng,
                PlaceType = placeType,
                PhotoReference = null
            };
                    

            if (result.Photos != null)
            {
                if (i < result.Photos.Count)
                {
                    newPlace.PhotoReference =
                        result.Photos[i].PhotoReference; // Set photoReference if it exists
                    
                }

                newPlaces.Add(newPlace);
                        
                for (var j = 0; j < result.Photos.Count; j++)
                {
                    var photo = result.Photos[j];

                    var parsedPhoto = new ParsedPlacePhoto()
                    {
                        PlaceId = newPlace.Id,
                        PhotoReference = newPlace.PhotoReference,
                        CreatedAt = DateTime.Now
                    };
                    parsedPhotos.Add(parsedPhoto);
                }
            }
        }


        await _mongoDb.AddManyPlacesAsync(newPlaces, cancellationToken);
        if (parsedPhotos.Count > 0)
            await _mongoDb.AddManyParsedPlacePhotos(parsedPhotos, cancellationToken);

        return newPlaces;
    }

}