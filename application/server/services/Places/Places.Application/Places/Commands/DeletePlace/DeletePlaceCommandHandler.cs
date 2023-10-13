using MediatR;
using Places.Application.Common.Exceptions;
using Places.Application.Interfaces;
using Places.Domain;

namespace Places.Application.Places.Commands.DeletePlace;

public class DeletePlaceCommandHandler : IRequestHandler<DeletePlaceCommand, Unit>
{
    private readonly IMongoDb _mongoDb;

    public DeletePlaceCommandHandler(IMongoDb mongoDb)
    {
        _mongoDb = mongoDb;
    }

    public async Task<Unit> Handle(DeletePlaceCommand request, CancellationToken cancellationToken)
    {
        var entity = await _mongoDb.PlaceExistsAsync(request.Id, cancellationToken);

        if (!entity) throw new NotFoundException(nameof(Place), request.Id);

        await _mongoDb.DeletePlaceAsync(request.Id, cancellationToken);
        return Unit.Value;
    }
}