using AutoMapper;
using Places.Domain;

namespace Places.Application.Likes.CreatePlaceLike;

public class PlaceLikeViewModel
{
    public string Id { get; set; }
    public string UserId { get; set; }
    public string PlaceId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }

    public void Mapping(Profile profile)
    {
        profile.CreateMap<PlaceLike, PlaceLikeViewModel>();
    }
}