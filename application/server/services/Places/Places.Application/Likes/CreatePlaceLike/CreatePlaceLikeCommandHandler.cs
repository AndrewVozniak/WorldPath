using AutoMapper;
using MediatR;
using Places.Application.Interfaces;
using Places.Domain;

namespace Places.Application.Likes.CreatePlaceLike;

public class CreatePlaceLikeCommandHandler : IRequestHandler<CreatePlaceLikeCommand, PlaceLikeViewModel>
{
    private readonly IMongoDb _mongoDb;
    private readonly IMapper _mapper;

    public CreatePlaceLikeCommandHandler(IMongoDb mongoDb, IMapper mapper)
    {
        _mongoDb = mongoDb;
        _mapper = mapper;
    }

    public async Task<PlaceLikeViewModel> Handle(CreatePlaceLikeCommand request, CancellationToken cancellationToken)
    {
        var placeLike = _mapper.Map<PlaceLike>(request);
        await _mongoDb.AddPlaceLikeAsync(placeLike, cancellationToken);
        return _mapper.Map<PlaceLikeViewModel>(placeLike);
    }
}