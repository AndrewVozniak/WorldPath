using FluentValidation;

namespace Places.Application.Places.Commands.UpdatePlace;

public class UpdatePlaceCommandValidator : AbstractValidator<UpdatePlaceCommand>
{
    public UpdatePlaceCommandValidator()
    {
        RuleFor(updatePlace =>
            updatePlace.Id).NotEmpty().NotEqual("");
        RuleFor(updatePlace =>
            updatePlace.Name).NotEmpty().NotEqual("");
        RuleFor(updatePlace =>
            updatePlace.Lat).NotEmpty();
        RuleFor(updatePlace =>
            updatePlace.Lon).NotEmpty();
        RuleFor(updatePlace =>
            updatePlace.PlaceType).NotEmpty();
    }
}