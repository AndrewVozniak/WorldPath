using MediatR;
using Places.Application.Dtos;
using Places.Domain;

namespace Places.Application.Places.Commands.CreateManyPlaces;

public class CreatePlacesCommand : IRequest<List<Place>>
{
    public PlacesApiResponse PlacesApiResponse { get; set; }
}