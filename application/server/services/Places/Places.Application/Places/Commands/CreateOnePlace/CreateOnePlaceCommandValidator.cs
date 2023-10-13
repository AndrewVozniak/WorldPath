using FluentValidation;

namespace Places.Application.Places.Commands.CreateOnePlace;

public class CreateOnePlaceCommandValidator : AbstractValidator<CreateOnePlaceCommand>
{
    public CreateOnePlaceCommandValidator()
    {
        RuleFor(createPlace =>
            createPlace.Id).NotEmpty().NotEqual("");
        RuleFor(createPlace =>
            createPlace.Name).NotEmpty().NotEqual("");
        RuleFor(createPlace =>
            createPlace.Lat).NotEmpty();
        RuleFor(createPlace =>
            createPlace.Lon).NotEmpty();
        RuleFor(createPlace =>
            createPlace.PlaceType).NotEmpty();
        RuleFor(createPlace =>
            createPlace.Location).NotEmpty();
    }
}