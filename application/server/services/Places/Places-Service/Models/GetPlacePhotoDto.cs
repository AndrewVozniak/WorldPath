using AutoMapper;
using Places.Application.Common.Mappings;
using Places.Application.PlacePhotos.Queries;

namespace Places_Service.Models;

public class GetPlacePhotoDto : IMapWith<GetPlacePhotoDto>
{
    public string PlaceId { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<GetPlacePhotoDto, GetPlacePhotosQuery>();
    }
}