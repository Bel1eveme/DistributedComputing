namespace Forum.Api.Models.Dto;

public class StoryRequestDto
{
    public long Id { get; set; }

    public string Title { get; set; } = string.Empty;
    
    public string Content { get; set; } = string.Empty;

    public DateTime Created { get; set; }

    public DateTime Modified  { get; set; }

    public Creator Creator { get; set; }
    
    public List<TagRequestDto> Tags { get; set; }
}