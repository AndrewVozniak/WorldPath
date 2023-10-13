using AutoMapper;
using MediatR;
using Places.Application.Interfaces;

namespace Places.Application.Places.Queries.GetPlaceById;

public class GetPlaceByIdQueryHandler : IRequestHandler<GetPlaceByIdQuery, PlaceByIdViewModel>
{
    private readonly IMapper _mapper;
    private readonly IMongoDb _mongoDb; 

    public GetPlaceByIdQueryHandler(IMapper mapper, IMongoDb mongoDb)
    {
        _mapper = mapper;
        _mongoDb = mongoDb;
    }

    public async Task<PlaceByIdViewModel> Handle(GetPlaceByIdQuery request, CancellationToken cancellationToken)
    {
        var place = await _mongoDb.GetPlaceById(request.Id, cancellationToken);

        return _mapper.Map<PlaceByIdViewModel>(place);
    }
}