using FluentValidation;
using Places.Application.Likes.CreatePlaceLike;

namespace Places.Application.Likes.Commands.CreatePlaceLike;

public class CreatePlaceLikeCommandValidator : AbstractValidator<CreatePlaceLikeCommand>
{
    public CreatePlaceLikeCommandValidator()
    {
        RuleFor(pl =>
            pl.UserId).NotEmpty().NotEqual("");
        RuleFor(pl =>
            pl.PlaceId).NotEmpty().NotEqual("");
    }
}