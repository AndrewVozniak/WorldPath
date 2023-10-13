using FluentValidation;

namespace Places.Application.Places.Queries.GetPlaceByCoordinate;

public class GetPlaceByCoordinateQueryValidator : AbstractValidator<GetPlaceByCoordinateQuery>
{
    public GetPlaceByCoordinateQueryValidator()
    {
        RuleFor(p =>
            p.Lat).NotEmpty();
        RuleFor(p =>
            p.Lon).NotEmpty();
        RuleFor(p =>
            p.Radius).NotEmpty();
    }
}