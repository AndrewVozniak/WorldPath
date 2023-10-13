using MediatR;

namespace Places.Application.Places.Commands.DeletePlace;

public class DeletePlaceCommand : IRequest<Unit>
{
    public string Id { get; set; }

    public DeletePlaceCommand(string id)
    {
        Id = id;
    }
}