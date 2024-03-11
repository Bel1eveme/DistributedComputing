namespace Forum.Api.Models;

public class Post
{
    public int Id { get; set; }

    public string Content { get; set; } = string.Empty;
    
    public Story Story { get; set; }
}