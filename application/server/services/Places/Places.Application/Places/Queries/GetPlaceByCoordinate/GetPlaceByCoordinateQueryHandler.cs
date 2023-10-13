using AutoMapper;
using MediatR;
using Places.Application.Common.Exceptions;
using Places.Application.Interfaces;
using Places.Domain;

namespace Places.Application.Places.Queries.GetPlaceByCoordinate;

public class GetPlaceByCoordinateQueryHandler : IRequestHandler<GetPlaceByCoordinateQuery, IEnumerable<PlaceByCoordinatesViewModel>>
{
    private readonly IMongoDb _mongoDb;
    private readonly IMapper _mapper;
    
    public GetPlaceByCoordinateQueryHandler(IMongoDb mongoDb, IMapper mapper)
    {
        _mongoDb = mongoDb;
        _mapper = mapper;
    }


    public async Task<IEnumerable<PlaceByCoordinatesViewModel>> Handle(GetPlaceByCoordinateQuery request, CancellationToken cancellationToken)
    {
        var place = await _mongoDb
            .FindPlacesNearbyAsync(request.Lat, request.Lon, request.Radius, cancellationToken);
        
        if (place == null)
        {
            throw new NotImplementedException();
        }

        return _mapper.Map<IEnumerable<PlaceByCoordinatesViewModel>>(place);
    }
}