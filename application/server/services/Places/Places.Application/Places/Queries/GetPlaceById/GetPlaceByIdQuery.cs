using MediatR;

namespace Places.Application.Places.Queries.GetPlaceById;

public class GetPlaceByIdQuery : IRequest<PlaceByIdViewModel>
{
    public string Id { get; set; }

    public GetPlaceByIdQuery(string id)
    {
        Id = id;
    }
}