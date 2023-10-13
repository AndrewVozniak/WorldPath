using MediatR;
using Places.Application.Dtos;
using Places.Domain;

namespace Places.Application.Places.Commands.CreateManyPlaces;

public class CreateManyPlacesCommand : IRequest<List<Place>>
{
    public List<Result> Results { get; set; }
    public string Status { get; set; }
}