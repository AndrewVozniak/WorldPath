using FluentValidation;

namespace Places.Application.Comments.Commands;

public class CreatePlaceCommentCommandValidator : AbstractValidator<CreatePlaceCommentCommand>
{
    public CreatePlaceCommentCommandValidator()
    {
        RuleFor(pc =>
            pc.PlaceId).NotEmpty().NotEqual("");
        RuleFor(pc =>
            pc.UserId).NotEmpty().NotEqual("");
        RuleFor(pc =>
            pc.Text).NotEmpty().NotEqual("");
    }
}