namespace Reviews.Dtos;

public class ReviewDto
{
    public required string UserId { get; set; }
    public float Rating { get; set; }
    public required string Text { get; set; }
}