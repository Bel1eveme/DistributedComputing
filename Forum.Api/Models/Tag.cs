namespace Forum.Api.Models;

public class Tag
{
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;
    
    public List<Story> Issues { get; set; }
}