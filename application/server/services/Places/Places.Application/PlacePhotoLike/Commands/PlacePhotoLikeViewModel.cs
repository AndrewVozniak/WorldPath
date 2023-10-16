namespace Places.Application.PlacePhotoLike.Commands;

public class PlacePhotoLikeViewModel
{
    public string Id { get; set; }
    public string PlacePhotoId { get; set; }
    public string UserId { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}