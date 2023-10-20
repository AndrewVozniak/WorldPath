using AutoMapper;
using MediatR;
using Places.Application.Common.Exceptions;
using Places.Application.Interfaces;
using Places.Domain;

namespace Places.Application.Comments.Commands;

public class CreatePlaceCommentCommandHandler : IRequestHandler<CreatePlaceCommentCommand, PlaceCommentViewModel>
{
    private readonly IMongoDb _mongoDb;
    private readonly IMapper _mapper;

    public CreatePlaceCommentCommandHandler(IMongoDb mongoDb, IMapper mapper)
    {
        _mongoDb = mongoDb;
        _mapper = mapper;
    }

    public async Task<PlaceCommentViewModel> Handle(CreatePlaceCommentCommand request, CancellationToken cancellationToken)
    {
        var isExists =  await _mongoDb.PlaceExistsAsync(request.PlaceId, cancellationToken);

        if (isExists == false) throw new NotFoundException(nameof(PlaceComment), request.PlaceId);

        var placeComment = _mapper.Map<PlaceComment>(request);
        await _mongoDb.AddPlaceCommentAsync(placeComment, cancellationToken);

        return _mapper.Map<PlaceCommentViewModel>(placeComment);
    }
}