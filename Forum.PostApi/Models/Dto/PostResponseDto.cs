namespace Forum.PostApi.Models.Dto;

public class PostResponseDto
{
    public Guid Id { get; set; }

    public string Content { get; set; } = string.Empty;
    
    public Guid StoryId { get; set; }
}