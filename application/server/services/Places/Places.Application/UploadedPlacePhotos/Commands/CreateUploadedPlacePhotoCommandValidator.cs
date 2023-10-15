using FluentValidation;
using MediatR;

namespace Places.Application.UploadedPlacePhotos.Commands;

public class CreateUploadedPlacePhotoCommandValidator : AbstractValidator<CreateUploadedPlacePhotoCommand>
{
    public CreateUploadedPlacePhotoCommandValidator()
    {
        RuleFor(up =>
            up.PlaceId).NotEmpty().NotEqual("");
        RuleFor(up =>
            up.PhotoData).NotEmpty();
    }
}