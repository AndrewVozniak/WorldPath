namespace Places.Application.PlacePhotoComment;

public class PlacePhotoCommentViewModel
{
    public string Id { get; set; }
    public string PlacePhotoId { get; set; }
    public string UserId { get; set; }
    public string Text { get; set; }
    public DateTime? UpdatedAt { get; set; }
    public DateTime CreatedAt { get; set; }
}