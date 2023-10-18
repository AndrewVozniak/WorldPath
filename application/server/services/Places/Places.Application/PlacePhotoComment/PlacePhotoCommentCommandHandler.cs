using AutoMapper;
using MediatR;
using Places.Application.Common.Exceptions;
using Places.Application.Interfaces;
using Places.Domain;

namespace Places.Application.PlacePhotoComment;

public class PlacePhotoCommentCommandHandler : IRequestHandler<PlacePhotoCommentCommand, PlacePhotoCommentViewModel>
{
    private readonly IMongoDb _mongoDb;
    private readonly IMapper _mapper;

    public PlacePhotoCommentCommandHandler(IMongoDb mongoDb, IMapper mapper)
    {
        _mongoDb = mongoDb;
        _mapper = mapper;
    }

    public async Task<PlacePhotoCommentViewModel> Handle(PlacePhotoCommentCommand request, CancellationToken cancellationToken)
    {
        var parsedPhoto = await _mongoDb.FindParsedPlacePhoto(request.PlacePhotoId, cancellationToken);

        if (parsedPhoto != null)
        {
            var comment = _mapper.Map<ParsedPlacePhotoComment>(request);
            await _mongoDb.CommentParsedPlacePhoto(comment);
            return _mapper.Map<PlacePhotoCommentViewModel>(comment);
        }

        var uploadedPhoto = await _mongoDb.FindUploadedPlacePhoto(request.PlacePhotoId, cancellationToken);

        if (uploadedPhoto != null)
        {
            var comment = _mapper.Map<UploadedPlacePhotoComment>(request);
            await _mongoDb.CommentUploadedPlacePhoto(comment);
            return _mapper.Map<PlacePhotoCommentViewModel>(comment);
        }

        throw new NotFoundException(nameof(PlacePhotoCommentViewModel), request.PlacePhotoId);
    }
}