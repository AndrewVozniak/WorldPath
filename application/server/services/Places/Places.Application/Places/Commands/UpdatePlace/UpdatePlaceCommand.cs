using MediatR;

namespace Places.Application.Places.Commands.UpdatePlace;

public class UpdatePlaceCommand : IRequest<Unit>
{
    public required string Id { get; set; }
    public required string Name { get; set; }
    public double Lat { get; set; }
    public double Lon { get; set; }
    public required string PlaceType { get; set; }
}