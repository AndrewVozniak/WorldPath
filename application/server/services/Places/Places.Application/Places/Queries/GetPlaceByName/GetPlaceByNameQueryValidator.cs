using FluentValidation;

namespace Places.Application.Places.Queries.GetPlaceByName;

public class GetPlaceByNameQueryValidator : AbstractValidator<GetPlaceByNameQuery>
{
    public GetPlaceByNameQueryValidator()
    {
        RuleFor(p =>
            p.Name).NotEmpty().NotEqual("");
    }
}