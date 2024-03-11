namespace Forum.Api.Models.Dto;

public class TagResponseDto
{
    public int Id { get; set; }

    public string Text { get; set; } = string.Empty;
    
    public List<StoryResponseDto> Issues { get; set; }
}