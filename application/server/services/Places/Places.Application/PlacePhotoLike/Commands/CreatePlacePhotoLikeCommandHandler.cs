using AutoMapper;
using MediatR;
using Places.Application.Common.Exceptions;
using Places.Application.Interfaces;
using Places.Domain;

namespace Places.Application.PlacePhotoLike.Commands;

public class CreatePlacePhotoLikeCommandHandler : IRequestHandler<CreatePlacePhotoLikeCommand, PlacePhotoLikeViewModel>
{
    private IMongoDb _mongoDb;
    private IMapper _mapper;

    public CreatePlacePhotoLikeCommandHandler(IMongoDb mongoDb, IMapper mapper)
    {
        _mongoDb = mongoDb;
        _mapper = mapper;
    }

    public async Task<PlacePhotoLikeViewModel> Handle(CreatePlacePhotoLikeCommand request, CancellationToken cancellationToken)
    {
        var parsedPhoto = await _mongoDb.FindParsedPlacePhoto(request.PlacePhotoId, 
            cancellationToken: cancellationToken);

        if (parsedPhoto != null)
        {
            var placePhotoLike = _mapper.Map<ParsedPlacePhotoLike>(request);
            await _mongoDb.LikeParsedPlacePhoto(placePhotoLike);
            return _mapper.Map<PlacePhotoLikeViewModel>(placePhotoLike);
        }

        var uploadedPhoto = await _mongoDb.FindUploadedPlacePhoto(request.PlacePhotoId,
            cancellationToken: cancellationToken);
        if (uploadedPhoto != null)
        {
            var placePhotoLike = _mapper.Map<UploadedPlacePhotoLike>(request);
            await _mongoDb.LikeUploadedPlacePhoto(placePhotoLike);
            return _mapper.Map<PlacePhotoLikeViewModel>(placePhotoLike);
        }

        throw new NotFoundException(nameof(PlacePhotoLikeViewModel), request.PlacePhotoId);
    }
}