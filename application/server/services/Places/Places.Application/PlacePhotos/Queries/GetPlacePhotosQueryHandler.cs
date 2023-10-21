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
        var placePhoto = new PlacePhotoViewModel();
        
        var parsedPhotos = await _mongoDb
            .GetAllParsedPlacePhoto(request.PlaceId, cancellationToken);

        if (parsedPhotos != null)
        {
            placePhoto.ParsedPlacePhotos = parsedPhotos;
        }
        
        var uploadedPhotos = await _mongoDb
            .GetAllUploadedPlacePhoto(request.PlaceId, cancellationToken);

        if (uploadedPhotos != null)
        {
            placePhoto.UploadedPlacePhotos = uploadedPhotos;
        }

        return placePhoto;
    }
}