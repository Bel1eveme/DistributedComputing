namespace Forum.Api.Models;

public class Story
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;

    public DateTime Created { get; set; }

    public DateTime Modified  { get; set; }

    public Creator Creator { get; set; }
    
    public List<Tag> Tags { get; set; }
}