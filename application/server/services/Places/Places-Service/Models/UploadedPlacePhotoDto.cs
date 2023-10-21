using AutoMapper;
using Places.Application.Common.Mappings;
using Places.Application.UploadedPlacePhotos.Commands;

namespace Places_Service.Models;

public class UploadedPlacePhotoDto : IMapWith<UploadedPlacePhotoDto>
{
    public string PlaceId { get; set; }
    public byte[] PhotoData { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<UploadedPlacePhotoDto, CreateUploadedPlacePhotoCommand>();
    }
}