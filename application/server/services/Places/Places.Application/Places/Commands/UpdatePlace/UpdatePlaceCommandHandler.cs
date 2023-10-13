using MediatR;
using Places.Application.Interfaces;

namespace Places.Application.Places.Commands.UpdatePlace;

public class UpdatePlaceCommandHandler : IRequestHandler<UpdatePlaceCommand, Unit>
{
    private readonly IMongoDb _mongoDb;

    public UpdatePlaceCommandHandler(IMongoDb mongoDb)
    {
        _mongoDb = mongoDb;
    }

    public async Task<Unit> Handle(UpdatePlaceCommand request, CancellationToken cancellationToken)
    {
        var success = await _mongoDb.UpdatePlaceAsync(request, cancellationToken);

        if (success == false) throw new NotImplementedException();

        return Unit.Value;
    }
}