namespace Forum.PostApi.Models;

public class Post
{
    public Guid Id { get; set; }
    
    public string Content { get; set; }
    
    public Guid StoryId { get; set; }
}