using AutoMapper;
using MediatR;
using Places.Application.Interfaces;

namespace Places.Application.Places.Queries.GetPlaceByName;

public class GetPlaceByNameQueryHandler : IRequestHandler<GetPlaceByNameQuery, PlaceByNameViewModel>
{
    private readonly IMongoDb _mongoDb;
    private readonly IMapper _mapper;
    
    public GetPlaceByNameQueryHandler(IMongoDb mongoDb, IMapper mapper)
    {
        _mongoDb = mongoDb;
        _mapper = mapper;
    }

    public async Task<PlaceByNameViewModel> Handle(GetPlaceByNameQuery request, CancellationToken cancellationToken)
    {
        var place = await _mongoDb.GetPlaceByName(request.Name, cancellationToken);

        if (place == null) throw new NotImplementedException();

        return _mapper.Map<PlaceByNameViewModel>(place);
    }
}