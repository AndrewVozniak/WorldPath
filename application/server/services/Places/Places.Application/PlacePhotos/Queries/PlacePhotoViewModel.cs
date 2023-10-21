using AutoMapper;
using Places.Application.Common.Mappings;
using Places.Domain;

namespace Places.Application.PlacePhotos.Queries;

public class PlacePhotoViewModel : IMapWith<PlacePhotoViewModel>
{
    public List<ParsedPlacePhoto> ParsedPlacePhotos { get; set; }
    public List<UploadedPlacePhoto> UploadedPlacePhotos { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<ParsedPlacePhoto, PlacePhotoViewModel>();
        profile.CreateMap<UploadedPlacePhoto, PlacePhotoViewModel>();
    }
}