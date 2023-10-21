using AutoMapper;
using Places.Application.Common.Mappings;
using Places.Application.PlacePhotos.Queries;
using Places.Domain;

namespace Places.Application.PlacePhotoLike.Commands;

public class PlacePhotoLikeViewModel : IMapWith<PlacePhotoLikeViewModel>
{
    public string Id { get; set; }
    public string PlacePhotoId { get; set; }
    public string UserId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ParsedPlacePhotoLike, PlacePhotoLikeViewModel>();
        profile.CreateMap<UploadedPlacePhotoLike, PlacePhotoLikeViewModel>();
    }
}