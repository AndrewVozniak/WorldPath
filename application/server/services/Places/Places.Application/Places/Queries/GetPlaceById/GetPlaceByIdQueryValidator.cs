using FluentValidation;

namespace Places.Application.Places.Queries.GetPlaceById;

public class GetPlaceByIdQueryValidator : AbstractValidator<GetPlaceByIdQuery>
{
    public GetPlaceByIdQueryValidator()
    {
        RuleFor(p =>
            p.Id).NotEmpty().NotEqual("");
    }
}