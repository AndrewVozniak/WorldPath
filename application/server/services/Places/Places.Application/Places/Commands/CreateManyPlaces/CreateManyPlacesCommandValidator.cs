using FluentValidation;

namespace Places.Application.Places.Commands.CreateManyPlaces;

public class CreateManyPlacesCommandValidator : AbstractValidator<CreateManyPlacesCommand>
{
    public CreateManyPlacesCommandValidator()
    {
        RuleFor(createPlaceCommand => 
            createPlaceCommand.Results).NotEmpty();
        RuleFor(createPlaceCommand =>
            createPlaceCommand.Status).NotEmpty();
    }
}