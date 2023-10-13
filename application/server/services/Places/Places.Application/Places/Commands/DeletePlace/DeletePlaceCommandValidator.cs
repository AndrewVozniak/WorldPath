using FluentValidation;

namespace Places.Application.Places.Commands.DeletePlace;

public class DeletePlaceCommandValidator : AbstractValidator<DeletePlaceCommand>
{
    public DeletePlaceCommandValidator()
    {
        RuleFor(deletePlace =>
            deletePlace.Id).NotEmpty().NotEqual("");
    }
}