namespace Forum.Api.Models.Dto;

public class TagRequestDto
{
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;
    
    public List<StoryRequestDto> Issues { get; set; }
}