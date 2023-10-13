using MediatR;

namespace Places.Application.Places.Queries.GetPlaceByName;

public class GetPlaceByNameQuery : IRequest<PlaceByNameViewModel>
{
    public string Name { get; set; }

    public GetPlaceByNameQuery(string name)
    {
        Name = name;
    }
}