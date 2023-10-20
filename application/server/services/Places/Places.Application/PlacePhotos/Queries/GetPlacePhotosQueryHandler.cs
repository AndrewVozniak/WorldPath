using AutoMapper;
using MediatR;
using Places.Application.Common.Exceptions;
using Places.Application.Interfaces;

namespace Places.Application.PlacePhotos.Queries;

public class GetPlacePhotosQueryHandler : IRequestHandler<GetPlacePhotosQuery, PlacePhotoViewModel>
{
    private readonly IMongoDb _mongoDb;
    private readonly IMapper _mapper;

    public GetPlacePhotosQueryHandler(IMongoDb mongoDb, IMapper mapper)
    {
        _mongoDb = mongoDb;
        _mapper = mapper;
    }

    public async Task<PlacePhotoViewModel> Handle(GetPlacePhotosQuery request, CancellationToken cancellationToken)
    {
        var parsedPhotos = await _mongoDb
            .GetAllParsedPlacePhoto(request.PlaceId, cancellationToken);

        if (parsedPhotos != null)
        {
            var photos = new PlacePhotoViewModel()
            {
                ParsedPlacePhotos = parsedPhotos
            };

            return photos;
        }

        var uploadedPhotos = await _mongoDb
            .GetAllUploadedPlacePhoto(request.PlaceId, cancellationToken);

        if (uploadedPhotos != null)
        {
            var photos = new PlacePhotoViewModel()
            {
                UploadedPlacePhotos = uploadedPhotos
            };

            return photos;
        }

        throw new NotFoundException(nameof(PlacePhotoViewModel), request.PlaceId);
    }
}